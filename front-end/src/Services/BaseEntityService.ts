import { IResultObject } from "../Types/IResultObject";
import { BaseService } from "./BaseService";
import { IPaginationObject } from "@/Types/IPaginationObject";

export abstract class BaseEntityService<TEntity> extends BaseService {
  private params: Record<string, string> = {};

  constructor(baseUrl: string, token?: string) {
    super(baseUrl, token);
  }

  async getAll(
    queryKey?: string,
    queryParams?: string,
  ): Promise<IResultObject<IPaginationObject<TEntity>>> {
    const params = { ...this.params };
    if (queryParams && queryKey) {
      params[queryKey] = queryParams;
    }
    try {
      const response = await this.httpClient.get<IPaginationObject<TEntity>>(
        "",
        {
          params,
        },
      );
      if (response.status < 300) {
        return { data: response.data };
      }
      return { errors: [this.handleError(response)] };
    } catch (error) {
      return { errors: [this.handleError(error)] };
    }
  }

  async get(id: string): Promise<IResultObject<TEntity>> {
    const params = { ...this.params };
    try {
      const response = await this.httpClient.get<TEntity>(id, { params });
      if (response.status < 300) {
        return { data: response.data };
      }
      return { errors: [this.handleError(response)] };
    } catch (error) {
      return { errors: [this.handleError(error)] };
    }
  }

  async create(data: TEntity): Promise<IResultObject<TEntity>> {
    const params = { ...this.params };
    try {
      const response = await this.httpClient.post("", data, { params });
      if (response.status < 300) {
        return { data: response.data };
      }
      return { errors: [this.handleError(response)] };
    } catch (error) {
      return { errors: [this.handleError(error)] };
    }
  }

  async delete(id: string): Promise<IResultObject<TEntity>> {
    const params = { ...this.params };
    try {
      const response = await this.httpClient.delete(id, { params });
      if (response.status < 300) {
        return {
          data: response.data,
        };
      }
      return { errors: [this.handleError(response)] };
    } catch (error) {
      return { errors: [this.handleError(error)] };
    }
  }

  async edit(data: TEntity, id: string): Promise<IResultObject<TEntity>> {
    const params = { ...this.params };
    try {
      const response = await this.httpClient.put<TEntity>(id, data, {
        params,
      });
      if (response.status < 300) {
        return {
          data: response.data,
        };
      }
      return { errors: [this.handleError(response)] };
    } catch (error) {
      return { errors: [this.handleError(error)] };
    }
  }

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  private handleError(error: any): string {
    if (error.response) {
      return `${error.response.data.title}`;
    } else {
      return `An error occurred: ${error.message}`;
    }
  }
}
