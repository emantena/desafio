import { Card } from './card.model';

export class Column {
  constructor(
    public statusId: number,
    public name: string,
    public active: boolean,
    public card: Card[]
  ) {}
}
