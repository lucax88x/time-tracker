import { addTodoAction, toggleTodoAction } from '../actions/todos';
import todos from './todos';

describe('todos reducer', () => {
  //   it('should handle initial state', () => {
  //     expect(todos(undefined, {})).toEqual([]);
  //   });

  it('should handle ADD_TODO', () => {
    expect(
      todos(
        {
          isTodosBusy: false,
          todos: []
        },
        addTodoAction(0, 'Run the tests')
      )
    ).toEqual({
      isTodosBusy: false,
      todos: [
        {
          completed: false,
          id: 0,
          title: 'Run the tests'
        }
      ]
    });
  });

  it('should handle TOGGLE_TODO', () => {
    expect(
      todos(
        {
          isTodosBusy: false,
          todos: [
            {
              completed: false,
              id: 1,
              title: 'Run the tests'
            },
            {
              completed: false,
              id: 0,
              title: 'Use Redux'
            }
          ]
        },
        toggleTodoAction(1)
      )
    ).toEqual({
      isTodosBusy: false,
      todos: [
        {
          completed: true,
          id: 1,
          title: 'Run the tests'
        },
        {
          completed: false,
          id: 0,
          title: 'Use Redux'
        }
      ]
    });
  });
});
