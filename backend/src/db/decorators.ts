/**
 * ERDCloud 상에서 정의된 도메인에 따라 type, length 등을 미리 지정한 컬럼 데코레이터
 * @see https://www.erdcloud.com/d/vgwjPSzzapScQxCEb
 */
import { Column, CreateDateColumn, JoinColumn, ObjectType, OneToOne, PrimaryColumn, PrimaryGeneratedColumn } from "typeorm";
import { CustomCodeType } from "./entity/custom-code-type";

/**
 * Auto-increment PK 타입에 대한 데코레이터를 제공합니다.
 * @param comment 코멘트
 * @returns 
 */
export const GeneratedId = (comment?: string) => PrimaryGeneratedColumn({ type: 'bigint', comment });

/**
 * PK 타입에 대한 데코레이터를 제공합니다.
 * 
 * 식별 관계에서 복합 키로 포함된 FK를 나타내는데 사용합니다.
 * @param comment 코멘트
 * @returns 
 */
export const Id = (comment?: string) => PrimaryColumn({ type: 'bigint', comment});

/**
 * 생성시간 컬럼에 대한 데코레이터를 제공합니다.
 * @param comment 코멘트
 * @returns 
 */
export const CreatedAt = (comment?: string) => CreateDateColumn({ type: 'datetime', comment });

/**
 * 삭제여부 컬럼에 대한 데코레이터를 제공합니다.
 * @param comment 코멘트
 * @returns 
 */
export const IsDeleted = (comment?: string) => Column({type: 'boolean', default: false, comment});

/**
 * 공통 코드 값을 나타내는 컬럼에 대한 데코레이터입니다.
 * @param type 코드 테이블의 엔티티 클래스
 */
export const Code = <T>(type: ObjectType<T>) => (
    (target: Object, propertyKey: string | symbol): void => {
        JoinColumn()(target, propertyKey);
        OneToOne(() => type)(target, propertyKey);
    }
);
