import axios, { AxiosRequestConfig } from 'axios';
import { Observable } from 'rxjs';

import { AxiosSubscriber } from './axios.subscriber';

class Rxios {
  public get<R>(url: string, queryParams?: object): Observable<R> {
    const cancelSource = axios.CancelToken.source();
    const config: AxiosRequestConfig = {
      cancelToken: cancelSource.token,
      params: queryParams
    };
    const request = axios.get<R>(url, config);
    return new Observable<R>(observer => {
      return new AxiosSubscriber<R>(observer, request, cancelSource);
    });
  }
}

export const rxios = new Rxios();
