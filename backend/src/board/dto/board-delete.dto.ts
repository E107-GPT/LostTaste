import { IsNotEmpty } from "class-validator";

export class BoardDeleteDto {
    @IsNotEmpty({
        message: '비밀번호는 필수입니다!'
    })
    password: string;
}