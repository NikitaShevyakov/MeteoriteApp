import { createApi} from "@reduxjs/toolkit/query/react";
import { buildQueryParams } from "../utils/buildQueryParams";
import { baseQueryWithNiceError } from './baseQuery';

const BASE_URL = "https://localhost:44370/api/v1/meteorites";

export const meteoriteApi = createApi({
  reducerPath: "meteoriteApi",
  baseQuery: baseQueryWithNiceError(BASE_URL),  
  endpoints: (builder) => ({
    getYears: builder.query({
      query: () => "/years",
    }),
    getClassifications: builder.query({
      query: () => "/classifications",
    }),
    getGroupedSummary: builder.query({
      query: (filters) => {
        const { page = 1, limit = 50, ...rest } = filters;
        return `summary?page=${page}&limit=${limit}&${buildQueryParams(rest)}`;
      }
    }),
  }),
});

export const {
  useGetYearsQuery,
  useGetClassificationsQuery,
  useGetGroupedSummaryQuery,
} = meteoriteApi;