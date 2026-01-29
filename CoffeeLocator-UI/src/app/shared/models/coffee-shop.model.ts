/**
 * Interface representing a Coffee Shop nearby the user.
 * Organized in a shared folder for reusability.
 */
export interface CoffeeShopNearby {
  id: string;
  name: string;
  address: string;
  latitude: number;
  longitude: number;
  distanceInKm: number;
  averageRating: number;
  totalReviews: number;
  isPremium: boolean;
  imageUrl?: string;
}