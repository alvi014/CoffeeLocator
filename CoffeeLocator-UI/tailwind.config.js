/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
  ],
  theme: {
    extend: {
      colors: {
        'coffee-dark': '#1a1a1a',
        'coffee-card': '#222222',
        'coffee-inner': '#262626',
        'coffee-gold': '#d99a4e',
      }
    },
  },
  plugins: [],
}