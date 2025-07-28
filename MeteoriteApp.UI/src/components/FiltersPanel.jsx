import { useMemo, useCallback } from "react";
import { Box, TextField, MenuItem } from "@mui/material";
import Autocomplete from '@mui/material/Autocomplete';

export default function FiltersPanel({ years = [], classifications = [], onFilterChange, currentFilters:filters }) {
  const yearOptions = useMemo(() => {
    const set = new Set(years.map(y => new Date(y).getFullYear()));
    return Array.from(set).sort((a, b) => a - b);
  }, [years]);

  const fromYearOptions = useMemo(() => {
    if (filters.toYear != null) {
      return yearOptions.filter(y => y <= filters.toYear);
    }
    return yearOptions;
  }, [yearOptions, filters.toYear]);

  const toYearOptions = useMemo(() => {
    if (filters.fromYear != null) {
      return yearOptions.filter(y => y >= filters.fromYear);
    }
    return yearOptions;
  }, [yearOptions, filters.fromYear]);

  const renderMenuItems = useCallback((options) =>
    options.map(year => (
      <MenuItem key={year} value={year}>{year}</MenuItem>
    )),[]);

  const handleFromYearChange = useCallback(
    (e) => onFilterChange({ fromYear: e.target.value === "" ? null : Number(e.target.value) }),
    [onFilterChange]
  );
  const handleToYearChange = useCallback(
    (e) => onFilterChange({ toYear: e.target.value === "" ? null : Number(e.target.value) }),
    [onFilterChange]
  );
  const handleClassificationChange = useCallback(
    (_, selected) => onFilterChange({ classificationCode: selected?.id ?? null }),
    [onFilterChange]
  );

  return (
    <Box display="flex" gap={2}>
      <TextField 
        fullWidth 
        label="Год с" 
        select 
        value={filters.fromYear ?? ""}
        onChange={handleFromYearChange}
      >
        <MenuItem value="">Any</MenuItem>
        {renderMenuItems(fromYearOptions)}
      </TextField>

      <TextField 
        fullWidth 
        label="Год по" 
        select 
        value={filters.toYear ?? ""}
        onChange={handleToYearChange}
      >
        <MenuItem value="">Any</MenuItem>
        {renderMenuItems(toYearOptions)}
      </TextField>

      <Autocomplete fullWidth
        disablePortal
        options={classifications}
        getOptionLabel={opt => opt.name}
        isOptionEqualToValue={(opt, val) => opt.id === val?.id}
        value={
            !classifications || classifications.length === 0
              ? null
              : (classifications.find(o => o.id === filters.classificationCode) || null)
          }
        onChange={handleClassificationChange}
        renderInput={(params) =>
          <TextField {...params} label="Класс метеорита" />}
      />
    </Box>
  );
}
