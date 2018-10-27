import { Action } from 'redux';
import { ActionsObservable, StateObservable } from 'redux-observable';
import { HotObservable } from 'rxjs/internal/testing/HotObservable';
import { TestScheduler } from 'rxjs/testing';

export function getTestScheduler() {
  return new TestScheduler((actual, expected) =>
    expect(actual).toEqual(expected)
  );
}

export function toActionObservable<A extends Action>(action: HotObservable<A>) {
  return ActionsObservable.from(action);
}

export function toStateObservable<S>(state: HotObservable<S>) {
  return new StateObservable(state, {});
}
