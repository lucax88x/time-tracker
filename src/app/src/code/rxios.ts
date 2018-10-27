import axios, { AxiosRequestConfig } from 'axios';
import { Observable } from 'rxjs';

import { AxiosSubscriber } from './axios.subscriber';

export interface IGraphQlResponse<T> {
  data: T;
}

class Rxios {
  public query<R, P = {}>(query: string, payload?: P): Observable<R> {
    return this.call<R, P>(query, payload);
  }
  public mutation<R, P>(mutation: string, payload: P): Observable<R> {
    return this.call<R, P>(mutation, payload);
  }

  public call<R, P = {}>(query: string, data?: P): Observable<R> {
    const cancelSource = axios.CancelToken.source();
    const config: AxiosRequestConfig = {
      cancelToken: cancelSource.token
    };

    const request = axios.post<IGraphQlResponse<R>>(
      'http://localhost:5000/graphql',
      { query, variables: data },
      config
    );

    return new Observable<R>(observer => {
      return new AxiosSubscriber<R>(observer, request, cancelSource);
    });
  }
}

export const rxios = new Rxios();
