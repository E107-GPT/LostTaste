import { HttpStatus } from "@nestjs/common";
import { ErrorHttpStatusCode } from "@nestjs/common/utils/http-error-by-code.util";

export class BusinessException extends Error {
    message: string;
    cause: Error | undefined;
    statusCode: ErrorHttpStatusCode;
}

const BusinessExceptionFactory = (statusCode: ErrorHttpStatusCode, defaultMessage: string) => (
    class extends BusinessException {
        constructor(data?: {
            message?: string,
            cause?: Error
        }) {
            super();
            this.statusCode = statusCode;
            this.message = data?.message ?? defaultMessage;
            this.cause = data?.cause;
        }
    }
);

export class DuplicatedIdException extends BusinessExceptionFactory(HttpStatus.CONFLICT, "ID가 중복됩니다.") {}

export class NoSuchContentException extends BusinessExceptionFactory(HttpStatus.NOT_FOUND, "해당하는 데이터가 없습니다.") {}

export class WrongPasswordException extends BusinessExceptionFactory(HttpStatus.FORBIDDEN, "비밀번호가 틀렸습니다.") {}