# 🏃 Sprint 2: User Experience & Authentication Flow

**Status:** In Progress 🚧 | **Objective:** "Closing the User Cycle"

## 🎯 Main Objectives
1.  **Identity:** Enable User Registration and Login (Real UI).
2.  **Navigation:** Implement a professional Layout (Header/Footer).
3.  **Interaction:** Transition from the Map view to a Detailed Coffee Shop view.

## 🛠️ Planned Tasks
- [ ] **UI/UX:** Create Login and Register components using Angular `ReactiveForms` and validations.
- [ ] **Shared:** Develop a `HeaderComponent` that reacts to the `AuthService` state (Display Login button vs. Username).
- [ ] **Features:** - Setup the `/coffee-shop/:id` route.
    - Implement a service to fetch a single coffee shop by ID.
- [ ] **Logic:** Implement an `AuthGuard` to protect authenticated-only routes.

## 🚀 Definition of Done (DoD)
- Users can navigate between the Map and their Profile without losing state.
- The Header is consistent throughout the entire application.
- Clear visual distinction between User and Admin options.