import { fetchBaseQuery } from '@reduxjs/toolkit/query/react';

export function baseQueryWithNiceError(baseUrl) {
  const rawBaseQuery = fetchBaseQuery({ baseUrl });
  return async (args, api, extraOptions) => {
    const result = await rawBaseQuery(args, api, extraOptions);
    return result.error ? { error: getErrorMessage(result.error) } : result;
  };
}

function getErrorMessage(err) {
   switch (true) {
    case err.status === 'FETCH_ERROR': return 'Не удалось соединиться с сервером. Проверьте интернет.';
    case err.status === 'PARSING_ERROR': return 'Ошибка обработки данных с сервера.';
    case typeof err.status === 'number' && err.status >= 500: return 'Сервер недоступен. Попробуйте позже.';
    case typeof err.status === 'number' && err.status >= 400: return err.data?.message ?? 'Некорректный запрос.';  
    case typeof err.error === 'string': return err.error;  
    default: return 'Что-то пошло не так';
  }
}