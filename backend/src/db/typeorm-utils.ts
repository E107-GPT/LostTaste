/**
 * ERD의 정의와 ERDCloud의 도메인을 기반으로 엔티티를 일괄 정의하기 위한 기능을 제공합니다.
 * @see https://www.erdcloud.com/d/vgwjPSzzapScQxCEb ERD
 * @author 구본웅
 */

import { Column, CreateDateColumn, JoinColumn, ManyToOne, ObjectType, PrimaryColumn, PrimaryGeneratedColumn } from "typeorm";
import { convertCamelToSnake } from "src/util/string-utils";

/**
 * 코드성 테이블과 매핑되는 엔티티임을 표시하기 위한 마커 인터페이스입니다. 
 */
export interface CodeTableEntity {}

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
 * 코드 엔티티와 연관관계를 맺는 컬럼에 대한 데코레이터입니다.
 * 
 * 내부적으로, 코드 테이블과 단방향 참조의 1:N 관계를 매핑합니다. 이 데코레이터가 적용된 컬럼이 연관 관계의 주인(owner)이 됩니다.
 * 
 * @param type 참조할 코드 테이블의 엔티티 클래스
 * @param name 테이블 상에서의 컬럼 이름. 생략하면 프로퍼티 이름이 snake_case로 변환되어 적용됩니다.
 */
export const CodeColumn = <T extends CodeTableEntity>(type: ObjectType<T>, name?: string) => (
    (target: Object, propertyKey: string | symbol): void => {
        let newColumnName = name ?? convertCamelToSnake(propertyKey.toString());

        JoinColumn({ name: newColumnName })(target, propertyKey);
        ManyToOne(() => type)(target, propertyKey);
    }
);