import { ExecutionContext, createParamDecorator } from "@nestjs/common";
import { JwtSignOptions } from "@nestjs/jwt";
import { UserDto } from "src/user/dto/user.dto";

/**
 * AuthGuard를 사용하는 컨트롤러 메소드의 파라미터에 UserDto를 주입합니다.
 */
export const AuthUser = createParamDecorator(
    (data: undefined, context: ExecutionContext): UserDto => {
        const request = context.switchToHttp().getRequest();
        return request['user'] as UserDto;
    }
);