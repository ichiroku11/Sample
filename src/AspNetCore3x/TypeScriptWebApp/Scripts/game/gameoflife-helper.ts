
type Range = (start: number, count: number) => number[];

/**
 * 指定した範囲内の整数の配列を生成する
 * @param start 最初の値
 * @param count 生成する整数の数
 */
export const range: Range = (start, count) => Array.from({ length: count }, (_, index) => start + index);
