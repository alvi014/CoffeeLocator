import {
  Component, EventEmitter, Input, OnInit, OnDestroy, Output
} from '@angular/core';
import { CommonModule, DecimalPipe } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import * as L from 'leaflet';
import { CoffeeShopService } from '../../../data/services/coffee-shop.service';
import { CoffeeShopNearby, CreateCoffeeShop, UpdateCoffeeShop } from '../../../shared/models/coffee-shop.model';

@Component({
  selector: 'app-coffee-shop-modal',
  standalone: true,
imports: [CommonModule, ReactiveFormsModule, FormsModule, DecimalPipe],
  templateUrl: './coffee-shop-modal.component.html',
  styleUrls: ['./coffee-shop-modal.component.css']
})
export class CoffeeShopModalComponent implements OnInit, OnDestroy {

  @Input() shop?: CoffeeShopNearby;
  @Output() closed = new EventEmitter<void>();
  @Output() saved  = new EventEmitter<void>();

  shopForm!: FormGroup;
  isSubmitting = false;
  submitError  = '';
  submitSuccess = false;

  // Map state
  mapReady = false;
  locating = false;
  searching = false;
  searchQuery = '';
  private map?: L.Map;
  private marker?: L.Marker;

  get isEditMode(): boolean { return !!this.shop; }

  constructor(private fb: FormBuilder, private coffeeService: CoffeeShopService) {}

  ngOnInit(): void {
    this.buildForm();
    this.fixLeafletIcons();
  }

  ngOnDestroy(): void {
    this.destroyMap();
  }

  // ── FORM ──────────────────────────────────────────────────────────────────

  private buildForm(): void {
    this.shopForm = this.fb.group({
      name:          [this.shop?.name     ?? '', [Validators.required, Validators.minLength(2)]],
      googlePlaceId: [''                       ],
      address:       [this.shop?.address  ?? '', [Validators.required]],
      description:   [''],
      imageUrl:      [this.shop?.imageUrl ?? ''],
      latitude:      [this.shop?.latitude  ?? null, [Validators.required, Validators.min(-90),  Validators.max(90)]],
      longitude:     [this.shop?.longitude ?? null, [Validators.required, Validators.min(-180), Validators.max(180)]],
      isPremium:     [this.shop?.isPremium ?? false],
    });
  }

  isInvalid(field: string): boolean {
    const c = this.shopForm.get(field);
    return !!(c?.invalid && c?.touched);
  }

  getCoordError(field: 'latitude' | 'longitude'): string {
    const c = this.shopForm.get(field);
    if (c?.hasError('required')) return 'Required';
    if (field === 'latitude'  && (c?.hasError('min') || c?.hasError('max'))) return '-90 to 90';
    if (field === 'longitude' && (c?.hasError('min') || c?.hasError('max'))) return '-180 to 180';
    return '';
  }

  // ── MAP ───────────────────────────────────────────────────────────────────

  toggleMap(): void {
    this.mapReady ? this.destroyMap() : this.initMap();
  }

  private initMap(): void {
    this.mapReady = true;

    setTimeout(() => {
      const lat = this.shopForm.value.latitude  ?? 9.9366;
      const lng = this.shopForm.value.longitude ?? -84.0633;

      this.map = L.map('modal-map', { zoomControl: true }).setView([lat, lng], 13);

      L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '© OpenStreetMap'
      }).addTo(this.map);

      if (this.shopForm.value.latitude && this.shopForm.value.longitude) {
        this.placeMarker(lat, lng);
      }

      this.map.on('click', (e: L.LeafletMouseEvent) => {
        this.placeMarker(e.latlng.lat, e.latlng.lng);
        this.shopForm.patchValue({
          latitude:  parseFloat(e.latlng.lat.toFixed(6)),
          longitude: parseFloat(e.latlng.lng.toFixed(6))
        });
        this.shopForm.get('latitude')?.markAsTouched();
        this.shopForm.get('longitude')?.markAsTouched();
      });

      setTimeout(() => this.map?.invalidateSize(), 100);
    }, 50);
  }
  
  // placeMarker adds a draggable marker to the map at the specified latitude and longitude. If a marker already exists, it removes it before placing the new one. It also sets up an event listener for the 'dragend' event, which updates the form's latitude and longitude values when the marker is moved.
  private placeMarker(lat: number, lng: number): void {
    if (this.marker) this.map?.removeLayer(this.marker);
    this.marker = L.marker([lat, lng], { draggable: true }).addTo(this.map!);

    this.marker.on('dragend', () => {
      const pos = this.marker!.getLatLng();
      this.shopForm.patchValue({
        latitude:  parseFloat(pos.lat.toFixed(6)),
        longitude: parseFloat(pos.lng.toFixed(6))
      });
    });
  }
 
  // Clean up the map instance to free resources and prevent memory leaks when the modal is closed or destroyed.
  private destroyMap(): void {
    if (this.map) {
      this.map.remove();
      this.map = undefined;
      this.marker = undefined;
    }
    this.mapReady = false;
  }

  // ── SEARCH LOCATION ───────────────────────────────────────────────────────

  searchLocation(): void {
    const query = this.searchQuery.trim();
    if (!query || this.searching) return;

    this.searching = true;

    // Nominatim API (OpenStreetMap geocoding - FREE)
    fetch(`https://nominatim.openstreetmap.org/search?format=json&q=${encodeURIComponent(query)}&limit=1`)
      .then(res => res.json())
      .then(data => {
        this.searching = false;
        if (data && data.length > 0) {
          const result = data[0];
          const lat = parseFloat(result.lat);
          const lng = parseFloat(result.lon);

          this.shopForm.patchValue({ latitude: lat, longitude: lng });

          if (this.map) {
            this.map.setView([lat, lng], 15);
            this.placeMarker(lat, lng);
          } else {
            this.initMap();
            setTimeout(() => {
              this.map?.setView([lat, lng], 15);
              this.placeMarker(lat, lng);
            }, 200);
          }
        } else {
          alert('Location not found. Try a different search term.');
        }
      })
      .catch(() => {
        this.searching = false;
        alert('Search failed. Check your internet connection.');
      });
  }

  // ── GEOLOCATION ───────────────────────────────────────────────────────────

  useMyLocation(): void {
    if (!navigator.geolocation) return;
    this.locating = true;
    navigator.geolocation.getCurrentPosition(
      (pos) => {
        const lat = parseFloat(pos.coords.latitude.toFixed(6));
        const lng = parseFloat(pos.coords.longitude.toFixed(6));
        this.shopForm.patchValue({ latitude: lat, longitude: lng });
        this.locating = false;
        if (this.map) {
          this.map.setView([lat, lng], 15);
          this.placeMarker(lat, lng);
        }
      },
      () => { this.locating = false; }
    );
  }

  // ── LEAFLET ICON FIX ──────────────────────────────────────────────────────

  private fixLeafletIcons(): void {
    const iconUrl       = 'https://unpkg.com/leaflet@1.7.1/dist/images/marker-icon.png';
    const iconRetinaUrl = 'https://unpkg.com/leaflet@1.7.1/dist/images/marker-icon-2x.png';
    const shadowUrl     = 'https://unpkg.com/leaflet@1.7.1/dist/images/marker-shadow.png';
    L.Marker.prototype.options.icon = L.icon({
      iconUrl, iconRetinaUrl, shadowUrl,
      iconSize: [25, 41], iconAnchor: [12, 41],
      popupAnchor: [1, -34], shadowSize: [41, 41]
    });
  }

  // ── SUBMIT ────────────────────────────────────────────────────────────────

  onSubmit(): void {
    if (this.shopForm.invalid) { this.shopForm.markAllAsTouched(); return; }
    this.isSubmitting = true;
    this.submitError  = '';
    this.submitSuccess = false;

    const v = this.shopForm.value;
    this.isEditMode ? this.handleUpdate(v) : this.handleCreate(v);
  }

  // Create takes the form values, constructs a CreateCoffeeShop payload, and calls the service to create a new shop. On success, it emits the saved event; on error, it shows an error message.
  private handleCreate(v: any): void {
    const payload: CreateCoffeeShop = {
      name:          v.name,
      googlePlaceId: v.googlePlaceId || undefined,
      address:       v.address,
      latitude:      Number(v.latitude),
      longitude:     Number(v.longitude),
      description:   v.description  || undefined,
      imageUrl:      v.imageUrl     || undefined,
    };
    this.coffeeService.createShop(payload).subscribe({
      next: () => { this.isSubmitting = false; this.submitSuccess = true; setTimeout(() => this.saved.emit(), 900); },
      error: (err) => { this.isSubmitting = false; this.submitError = err?.error?.message ?? 'Could not create shop.'; }
    });
  }

  // Update is similar to create but includes the shop ID and calls updateShop() instead of createShop()
  private handleUpdate(v: any): void {
    const payload: UpdateCoffeeShop = {
      id:            this.shop!.id,
      name:          v.name,
      googlePlaceId: v.googlePlaceId || undefined,
      address:       v.address,
      latitude:      Number(v.latitude),
      longitude:     Number(v.longitude),
      description:   v.description  || undefined,
      imageUrl:      v.imageUrl     || undefined,
      isPremium:     v.isPremium,
    };
    this.coffeeService.updateShop(this.shop!.id, payload).subscribe({
      next: () => { this.isSubmitting = false; this.submitSuccess = true; setTimeout(() => this.saved.emit(), 900); },
      error: (err) => { this.isSubmitting = false; this.submitError = err?.error?.message ?? 'Could not update shop.'; }
    });
  }

  // ── MODAL CONTROL ─────────────────────────────────────────────────────────

  close(): void { this.closed.emit(); }

  onBackdropClick(event: MouseEvent): void {
    if ((event.target as HTMLElement).classList.contains('modal-backdrop')) this.close();
  }
}