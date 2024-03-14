import { HttpStatus } from "@nestjs/common";
import { ErrorHttpStatusCode } from "@nestjs/common/utils/http-error-by-code.util";

export const BusinessException = (statusCode: ErrorHttpStatusCode, defaultMessage: string) => (
    class extends Error {
        message: string;
        cause: Error | undefined;
        statusCode: ErrorHttpStatusCode;
    
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

export class DuplicatedIdException extends BusinessException(HttpStatus.CONFLICT, "ID가 중복됩니다.") {}