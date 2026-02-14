# 🏃 Sprint 2: User Experience & Authentication Flow

**Status:** In Progress 🚧 | **Objective:** "Closing the User Cycle"

## 🎯 Main Objectives
1.  **Identity:** Enable User Registration and Login (Real UI). ✅ (Login Real Functional)
2.  **Navigation:** Implement a professional Layout (Header/Footer). 🚧 (In progress)
3.  **Interaction:** Transition from the Map view to a Detailed Coffee Shop view. ⏳ (Pending)

## 🛠️ Planned Tasks
- [x] **UI/UX:** Create Login component using Angular `ReactiveForms` and validations.
- [x] **UI/UX:** Create Register component (Pending).
- [x] **Logic:** Implement an `AuthGuard` to protect authenticated-only routes.
- [x] **Connectivity:** Connect `AuthService` with real .NET Backend (Endpoint: `/api/auth/login`).
- [x] **Shared:** Update `HeaderComponent` to react to `AuthService` state (Display Logout button vs Login).
- [x] **Features:** - Setup the `/coffee-shop/:id` route.
    - Implement a service to fetch a single coffee shop by ID.

## 🚀 Definition of Done (DoD)
- [x] Users can navigate between the Map and their Profile without losing state.
- [x] The Header is consistent and dynamic (shows/hides options based on Auth).
- [x] Registration flow is complete and saves users in the DB.
- [x] Clicking a marker on the map redirects to the correct Detail view.