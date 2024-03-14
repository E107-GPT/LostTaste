import { HttpStatus } from "@nestjs/common";
import { ErrorHttpStatusCode } from "@nestjs/common/utils/http-error-by-code.util";

export class BusinessException extends Error {
    message: string;
    cause: Error | undefined;
    statusCode: ErrorHttpStatusCode;

    constructor(data?: Partial<BusinessException>) {
        super();
        this.message = data?.message ?? "비즈니스 예외 발생";
        this.cause = data?.cause;
        this.statusCode = data?.statusCode ?? HttpStatus.INTERNAL_SERVER_ERROR;
    }
}