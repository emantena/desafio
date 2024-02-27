export class Card {
  constructor(
    public cardId: number,
    public statusId: number,
    public name: string,
    public description: string,
    public createAt: Date
  ) {}
}
