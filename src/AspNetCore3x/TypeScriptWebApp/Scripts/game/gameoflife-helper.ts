
type Range = (start: number, count: number) => number[];

export const range: Range = (start, count) => Array.from({ length: count }, (_, index) => start + index);
