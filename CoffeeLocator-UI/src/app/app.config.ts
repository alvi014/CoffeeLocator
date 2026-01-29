import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';

import { routes } from './app.routes';

/**
 * Global application configuration for Angular Standalone.
 * This replaces the traditional AppModule structure.
 */
export const appConfig: ApplicationConfig = {
  providers: [
    /**
     * Enables Zone.js change detection with event coalescing 
     * for better performance in complex UIs.
     */
    provideZoneChangeDetection({ eventCoalescing: true }),

    /**
     * Registers the application routes defined in app.routes.ts.
     */
    provideRouter(routes),

    /**
     * Provides the HttpClient service globally, allowing components
     * and services to communicate with the C# Backend.
     */
    provideHttpClient()
  ]
};