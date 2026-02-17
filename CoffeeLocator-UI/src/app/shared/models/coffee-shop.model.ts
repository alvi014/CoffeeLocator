
/**
 * The CoffeeShopNearby interface represents the structure of a coffee shop that is nearby, including its ID, name, address, location coordinates, distance from the user, average rating, total reviews, premium status, and an optional image URL.
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

/**
 * The CoffeeShopDetail interface extends CoffeeShopNearby and includes additional fields for a detailed view of a coffee shop, such as description, Google Place ID, and recent reviews.
 */
export interface CoffeeShopDetail extends CoffeeShopNearby {
  description: string;
  googlePlaceId: string;
  recentReviews: ReviewResponse[];
}

/**
 * The CreateCoffeeShop interface represents the structure of a new coffee shop to be created.
 */
export interface CreateCoffeeShop {
  name: string;
  googlePlaceId: string;
  address: string;
  latitude: number;
  longitude: number;
  description?: string;
  imageUrl?: string;
}

/**
 * The UpdateCoffeeShop interface extends CreateCoffeeShop and includes additional fields for updating an existing coffee shop.
 */
export interface UpdateCoffeeShop extends CreateCoffeeShop {
  id: string;
  isPremium: boolean;
}

/**
 * The ReviewResponse interface represents the structure of a review for a coffee shop, including the reviewer's information, comment, rating, and creation date.
 */
export interface ReviewResponse {
  id: string;
  createdBy: string;
  comment: string;
  rating: number;
  createdAt: Date;
}