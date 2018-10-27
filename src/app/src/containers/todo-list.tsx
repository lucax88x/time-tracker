import { connect } from 'react-redux';
import { Dispatch } from 'redux';

import { TodosActions } from '../actions';
import { getTodosAction, toggleTodoAction } from '../actions/todos';
import {
  ITodoListDispatches,
  ITodoListProps,
  TodoList
} from '../components/todo-list';
import { selectIsTodosBusy, selectTodos } from '../selectors/todos';
import { State } from '../states/state';

const mapStateToProps = (state: State): ITodoListProps => ({
  isBusy: selectIsTodosBusy(state),
  todos: selectTodos(state)
});

const mapDispatchToProps = (
  dispatch: Dispatch<TodosActions>
): ITodoListDispatches => ({
  getTodos: () => dispatch(getTodosAction()),
  toggleTodo: (id: number) => dispatch(toggleTodoAction(id))
});

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(TodoList);
