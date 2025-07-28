import { Typography} from '@mui/material';

export default function ErrorMessage({ error }) {
  return (
    <Typography color="error" sx={{ m: 4 }}>
      Ошибка: {error.toString()}
    </Typography>
  );
}