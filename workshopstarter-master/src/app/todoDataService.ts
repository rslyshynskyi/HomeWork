import { Injectable } from '@angular/core';
import { Todo } from './todo';

@Injectable()
export class TodoDataService {
    lastId: number = 0;
    todos: Todo[] = [];

    constructor () {

    }

    addTodo(todo: Todo) {
        if (!todo.id) {
            todo.id = ++this.lastId;
        }

        this.todos.push(todo);
    }

    deleteTodoById(id: number) {
        this.todos = this.todos.filter(todo => todo.id !== id);
    }

    updateTodoById(id: number, values: Object = {}): Todo {
        let todo = this.getTodoById(id);

        if (todo === null) {
            return null;
        }

        Object.assign(todo, values);
        return todo;
    }

    getAllTodos(): Todo[] {
        return this.todos;
    }

    getTodoById(id: number): Todo {
        return this.todos.filter(todo => todo.id === id).pop();
    }

    toggleTodoComplete(todo: Todo) {
        let updateTodo = this.updateTodoById(todo.id, { complete: !todo.complete });

        return updateTodo;
    }
}
