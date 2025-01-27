import { BaseEntityService } from "./BaseEntityService";

export default class ToDoService<TEntity> extends BaseEntityService<TEntity> {
  constructor(action: string, token?: string) {
    super("" + action, token);
  }
}
