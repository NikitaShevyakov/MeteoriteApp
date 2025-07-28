import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  filters: {
    fromYear:           null,
    toYear:             null,
    classificationCode: null,
    nameContains:       "",
    page:               1,
    limit:              20,
    sortBy:             "year",
    sortOrder:          "asc"
  }
};

const meteoritesSlice = createSlice({
  name: "meteorites",
  initialState: initialState,
  reducers: {
    setFilters(state, action) {
      const prev = state.filters;
      const next = { ...prev, ...action.payload };

      const filterKeys = ["fromYear", "toYear", "classifications", "nameContains"];
      const resetPage = filterKeys.some(
        key => action.payload[key] !== undefined && action.payload[key] !== prev[key]
      );
      state.filters = {
        ...next,
        page: resetPage ? 1 : next.page
      };
    },
    setPage(state, action) {
      state.filters.page = action.payload;
    },
    setLimit(state, action) {
      state.filters.limit = action.payload;
    },
    setSortBy(state, action) {
      state.filters.sortBy = action.payload;
      state.filters.page = 1;
    },
    setSortOrder(state, action) {
      state.filters.sortOrder = action.payload;
      state.filters.page = 1;
    },
    resetFilters(state) {
      state.filters = { ...initialState.filters };
    }
  }
});

export const {
  setFilters,
  setPage,
  setLimit,
  setSortBy,
  setSortOrder,
  resetFilters
} = meteoritesSlice.actions;

export default meteoritesSlice.reducer;
