import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CoffeeShopForm } from './coffee-shop-form';

describe('CoffeeShopForm', () => {
  let component: CoffeeShopForm;
  let fixture: ComponentFixture<CoffeeShopForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CoffeeShopForm]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CoffeeShopForm);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
