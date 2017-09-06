import { Component } from '@angular/core';
import { Todo } from './todo';
import { TodoDataService } from './todoDataService';

@Component({
  selector: 'my-app',
  templateUrl: './app.component.html',
  styleUrls: [ './app.component.css'],
  providers: [ TodoDataService ]
})
export class AppComponent  {
  newTodo: Todo = new Todo;
  constructor( private todoDataService: TodoDataService) {

  }

  addTodo() {
    this.todoDataService.addTodo(this.newTodo);
    this.newTodo = new Todo();
  }

  toggleTodoComplete(todo: Todo) {
    this.todoDataService.toggleTodoComplete(todo);
  }

  removeTodo(todo: Todo) {
    this.todoDataService.deleteTodoById(todo.id);
  }

  get todos() {
    return this.todoDataService.getAllTodos();
  }
}
