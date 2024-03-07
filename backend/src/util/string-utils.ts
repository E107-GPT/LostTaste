/**
 * 문자열 관련 편의 함수를 제공합니다.
 * @author 구본웅
 */

/**
 * `camelCase`를 `snake_case`로 변환합니다.
 * @param str 변환할 camelCase 문자열
 * @returns 변환된 snake_case 문자열
 */
export const convertCamelToSnake = (str: string) => str.replace(/[A-Z]/g, letter => `_${letter.toLowerCase()}`); 