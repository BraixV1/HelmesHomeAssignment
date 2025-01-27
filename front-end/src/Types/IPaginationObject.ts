export interface IPaginationObject<TEntity> {
  items: TEntity[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
}
