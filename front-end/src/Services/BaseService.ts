import axios, { AxiosInstance } from "axios";

export abstract class BaseService {
  private static hostBaseURL = process.env.NEXT_PUBLIC_APIENDPOINT;
  protected httpClient: AxiosInstance;

  constructor(baseUrl: string, accessToken?: string) {
    this.httpClient = axios.create({
      baseURL: BaseService.hostBaseURL + baseUrl,
      headers: {
        "Content-Type": "application/json",
        ...(accessToken && { Authorization: `Bearer ${accessToken}` }),
      },
    });
  }
}
