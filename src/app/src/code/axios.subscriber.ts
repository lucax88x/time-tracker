import { AxiosPromise, CancelTokenSource } from 'axios';
import { Subscriber } from 'rxjs';

import { IGraphQlResponse } from './rxios';

export class AxiosSubscriber<T> extends Subscriber<T> {
  constructor(
    observer: Subscriber<T>,
    request: AxiosPromise<IGraphQlResponse<T>>,
    private cancelSource: CancelTokenSource
  ) {
    super(observer);

    request
      .then(response => {
        this.next(response.data.data);
        this.complete();
      })
      .catch((err: Error) => {
        this.error(err);
        this.complete();
      });
  }

  public unsubscribe() {
    super.unsubscribe();
    this.cancelSource.cancel('Operation canceled by the user.');
  }
}
