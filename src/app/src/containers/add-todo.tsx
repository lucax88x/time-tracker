import { connect } from 'react-redux';
import { Dispatch } from 'redux';

import { TodosActions } from '../actions';
import { addTodoAction } from '../actions/todos';
import { AddTodo, IAddTodoDispatches } from '../components/add-todo';

const mapStateToProps = () => ({});

const mapDispatchToProps = (
  dispatch: Dispatch<TodosActions>
): IAddTodoDispatches => ({
  addTodo: (value: string) => dispatch(addTodoAction(0, value))
});

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(AddTodo);
