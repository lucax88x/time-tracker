import uuidv4 from 'uuid/v4';

export class UUID {
  public static Empty = '00000000-0000-0000-0000-000000000000';
  public static Generate(): UUID {
    return new UUID(uuidv4());
  }

  public value: string = UUID.Empty;

  constructor(value: string | undefined) {
    this.value = !!value ? value : UUID.Empty;
  }

  public get isEmpty() {
    return this.value === UUID.Empty;
  }

  public toString = (): string => {
    return this.value;
  };
}
