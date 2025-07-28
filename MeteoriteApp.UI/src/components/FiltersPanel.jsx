import { Box, TextField, MenuItem } from "@mui/material";
import Autocomplete from '@mui/material/Autocomplete';

export default function FiltersPanel({ years = [], classifications = [], onFilterChange, currentFilters:filters }) {

  return (
    <Box display="flex" gap={2}>
      <TextField 
        fullWidth 
        label="Год с" 
        select 
        value={filters.fromYear ?? ""}
        onChange={(e) =>onFilterChange({ fromYear:e.target.value === "" ? null : Number(e.target.value) })}
      >
        <MenuItem value="">Any</MenuItem>
        {years.map((y) => {
          const year = new Date(y).getFullYear();
          return (<MenuItem key={year} value={year}>{year}</MenuItem>);
        }
        )}
      </TextField>

      <TextField 
        fullWidth 
        label="Год по" 
        select 
        value={filters.toYear ?? ""}
        onChange={(e) =>onFilterChange({ toYear: e.target.value === "" ? null : Number(e.target.value)})}
      >
        <MenuItem value="">Any</MenuItem>
        {years.map((y) => {
          const year = new Date(y).getFullYear();
          return (<MenuItem key={year} value={year}>{year}</MenuItem>);
        }
        )}
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
        onChange={(_, selected) => {
          onFilterChange({ classificationCode: selected?.id ?? null });
        }}
        
        renderInput={(params) =>
          <TextField {...params} label="Класс метеорита" />}
      />
      {/* <TextField fullWidth label="Class" select onChange={(e) => onFilterChange({ classifications: e.target.value })}>
        <MenuItem value="">Any</MenuItem>
        {classifications.map((c) => <MenuItem value={c.id}>{c.name}</MenuItem>)}
      </TextField> */}
      {/* <TextField label="Название" onChange={(e) =>
        // dispatch(setFilters({ nameContains: e.target.value }))
      } /> */}
    </Box>
  );
}
