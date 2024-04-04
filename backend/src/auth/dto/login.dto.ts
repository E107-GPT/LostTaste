import { IsAlphanumeric, IsNotEmpty } from "class-validator";

export class LoginDto {
    @IsAlphanumeric('en-US', {
        message: 'ID는 영문자 또는 숫자여야 합니다!'
    })
    accountId: string;

    @IsNotEmpty({
        message: '비밀번호는 필수입니다!'
    })
    password: string;
}