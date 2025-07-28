import { Container, Typography, Box } from "@mui/material";
import { useDispatch, useSelector } from "react-redux";

import FiltersPanel from "../components/FiltersPanel";
import MeteoriteTable from "../components/MeteoriteTable";
import Spinner from "../components/Spiner";
import ErrorMessage from "../components/ErrorMessage";

import {
  useGetYearsQuery,
  useGetClassificationsQuery,
  useGetGroupedSummaryQuery,
} from "../api/meteoriteApi";
import {
  setFilters,
  setPage,
  setLimit,
  setSortBy,
  setSortOrder
} from "../redux/meteoritesSlice";

export default function MainPage() {
  const dispatch = useDispatch();
  const filters = useSelector((state) => state.meteorites.filters);

  const { data: years, isFetching: isFetchingYears, error: yearsError } = useGetYearsQuery();
  const { data: classifications, isFetching: isClassifications, error: classificationsError } = useGetClassificationsQuery();
  const { data: summary, isFetchingSummary, error: summaryError } = useGetGroupedSummaryQuery(filters);

  if (isClassifications || isFetchingYears || isFetchingSummary) return <Spinner />;
  const loadError = yearsError || classificationsError || summaryError;
  if (loadError) return <ErrorMessage error={loadError} />;

  const rows = summary?.data ?? [];
  const total = summary?.total ?? 0;

  const handleSort = (field) => {
    if (filters.sortBy === field) {
      dispatch(setSortOrder(filters.sortOrder === "asc" ? "desc" : "asc"));
    } else {
      dispatch(setSortBy(field));
    }
  };

  return (
    <Container maxWidth="xl">
      <Typography variant="h4" gutterBottom sx={{ mt: 4 }}>
        Meteorite Dashboard
      </Typography>

      <Box mb={3}>
        <FiltersPanel
          years={years}
          classifications={classifications}
          onFilterChange={(newF) => dispatch(setFilters(newF))}
          currentFilters={filters}
        />
      </Box>

      <MeteoriteTable
        rows={rows}
        total={total}
        filters={filters}
        onSort={handleSort}
        onPageChange={(page) => dispatch(setPage(page))}
        onRowsPerPageChange={(limit) => {
          dispatch(setLimit(limit));
          dispatch(setPage(1));
        }}
      />
    </Container>
  );
}