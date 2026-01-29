import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';

import { CoffeeShopService } from './coffee-shop.service';

describe('CoffeeShopService', () => {
  let service: CoffeeShopService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [provideHttpClient(), provideHttpClientTesting()]
    });
    service = TestBed.inject(CoffeeShopService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
