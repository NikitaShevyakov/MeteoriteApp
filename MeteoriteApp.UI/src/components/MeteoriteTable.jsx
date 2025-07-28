import { useRef,useMemo } from 'react';
import {
  Paper, Table, TableHead, TableBody, TableFooter, Box,
  TableRow, TableCell, TablePagination,
  TableSortLabel 
} from '@mui/material';
import {ScrollArea, tableSx, cellSx, TotalsRow} from './MeteoriteTableStyles';
import { CustomScrollbar } from './CustomScrollbar';

export default function MeteoriteTable({
  rows, total, filters,
  onPageChange, onRowsPerPageChange,
  onSort
}) {
  const scrollRef = useRef(null);

  // данные текущей страницы
  const page = filters.page - 1;
  const rowsPerPage = filters.limit;
  const displayedRows = rows;
  const [totalCountOnPage, totalMassOnPage] = useMemo(() => {
    let count = 0, mass = 0;
    for (const r of displayedRows) {
      count += r.count;
      mass += r.mass;
    }
    return [count, mass];
  }, [displayedRows]);

  // размеры контейнера скролла
  const HEADER = 56, FOOTER = 72;
  const SCROLL_H = `calc(100vh - 220px - ${HEADER}px - ${FOOTER}px)`;

  return (
   <Paper sx={{ width: '100%', mx: 'auto' }}>
      <Table stickyHeader sx={tableSx}>
        <TableHead>
          <TableRow>
            {[
              { id: "year", label: "Год" },
              { id: "count", label: "Количество метеоритов" },
              { id: "mass",  label: "Суммарная масса" }
            ].map(column => (
              <TableCell
                key={column.id}
                align="center"
                sx={cellSx}
                sortDirection={filters.sortBy === column.id ? filters.sortOrder : false}
              >
                <TableSortLabel
                  active={filters.sortBy === column.id}
                  direction={filters.sortBy === column.id ? filters.sortOrder : "asc"}
                  onClick={() => onSort(column.id)}
                >
                  {column.label}
                </TableSortLabel>
              </TableCell>
            ))}
          </TableRow>
        </TableHead>
      </Table>


      <Box sx={{ height: SCROLL_H, position: 'relative' }}>
        <ScrollArea ref={scrollRef} sx={{ height: '100%' }}>
          <Table sx={tableSx}>
            <TableBody>
              {displayedRows.map(({ date, count, mass }) => {
                const year = new Date(date).getFullYear();
                return (
                  <TableRow hover key={date}>
                    <TableCell align="center" sx={cellSx}>{year}</TableCell>
                    <TableCell align="center" sx={cellSx}>{count}</TableCell>
                    <TableCell align="center" sx={cellSx}>{mass}</TableCell>
                  </TableRow>
                );
              })}
            </TableBody>
          </Table>
        </ScrollArea>
        <CustomScrollbar containerRef={scrollRef} rows={displayedRows} />
      </Box>

      <Table sx={tableSx}>
        <TableFooter>
          <TotalsRow>
            <TableCell align="center" sx={cellSx}>Итого на странице</TableCell>
            <TableCell align="center" sx={cellSx}>{totalCountOnPage}</TableCell>
            <TableCell align="center" sx={cellSx}>{totalMassOnPage}</TableCell>
          </TotalsRow>
          <TableRow>
            <TableCell colSpan={3} sx={{ p: 0 }}>
              <TablePagination
                rowsPerPageOptions={[10, 20, 50, 100]}
                component="div"
                count={total}
                rowsPerPage={rowsPerPage}
                page={page}
                onPageChange={(_, p) => onPageChange(p + 1)}
                onRowsPerPageChange={e =>
                  onRowsPerPageChange(+e.target.value)
                }
                labelRowsPerPage="На странице:"
                labelDisplayedRows={({ from, to, count }) =>
                  `${from}–${to} из ${count}`
                }
                showFirstButton
                showLastButton
              />
            </TableCell>
          </TableRow>
        </TableFooter>
      </Table>
    </Paper>
  );
}


