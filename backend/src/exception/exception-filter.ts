import { ArgumentsHost, Catch, ExceptionFilter } from "@nestjs/common";
import { BusinessException } from "./exception";

@Catch(BusinessException)
export class BusinessExceptionFilter implements ExceptionFilter<BusinessException> {
    catch(exception: BusinessException, host: ArgumentsHost) {
        const ctx = host.switchToHttp();
        const response = ctx.getResponse();

        response
            .status(exception.statusCode)
            .json({
                message: [exception.message],
                error: exception.name,
                statusCode: exception.statusCode
            })
    }
    
}