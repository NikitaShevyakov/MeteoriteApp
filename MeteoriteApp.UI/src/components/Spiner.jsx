import {Paper,  CircularProgress, Box} from '@mui/material';

export default function Spinner() {
  return (
    <Box
      sx={{
        width: '100vw',
        height: '100vh',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        overflow: 'hidden',
      }}
    >
      <Paper sx={{ p: 4, textAlign: 'center', backgroundColor: 'rgb(247 248 248)' }} elevation={0} >
        <CircularProgress />
      </Paper>
    </Box>
  );
}