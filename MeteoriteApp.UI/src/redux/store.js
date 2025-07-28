import { configureStore } from "@reduxjs/toolkit";
import { meteoriteApi } from "../api/meteoriteApi";
import meteoritesReducer from "../redux/meteoritesSlice"; 

export const store = configureStore({
  reducer: {
    [meteoriteApi.reducerPath]: meteoriteApi.reducer,
    meteorites: meteoritesReducer
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(meteoriteApi.middleware),
  devTools: process.env.NODE_ENV !== "production" 
});