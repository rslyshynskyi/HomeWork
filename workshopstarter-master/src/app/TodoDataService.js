"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require("@angular/core");
var TodoDataService = (function () {
    function TodoDataService() {
        this.lastId = 0;
        this.todos = [];
    }
    TodoDataService.prototype.addTodo = function (todo) {
        if (!todo.id) {
            todo.id = ++this.lastId;
        }
        this.todos.push(todo);
    };
    TodoDataService.prototype.deleteTodoById = function (id) {
        this.todos = this.todos.filter(function (todo) { return todo.id !== id; });
    };
    TodoDataService.prototype.updateTodoById = function (id, values) {
        if (values === void 0) { values = {}; }
        var todo = this.getTodoById(id);
        if (todo === null) {
            return null;
        }
        Object.assign(todo, values);
        return todo;
    };
    TodoDataService.prototype.getAllTodos = function () {
        return this.todos;
    };
    TodoDataService.prototype.getTodoById = function (id) {
        return this.todos.filter(function (todo) { return todo.id === id; }).pop();
    };
    TodoDataService.prototype.toggleTodoComplete = function (todo) {
        var updateTodo = this.updateTodoById(todo.id, { complete: !todo.complete });
        return updateTodo;
    };
    return TodoDataService;
}());
TodoDataService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [])
], TodoDataService);
exports.TodoDataService = TodoDataService;
//# sourceMappingURL=todoDataService.js.map