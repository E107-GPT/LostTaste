import { IsAlphanumeric, Length, Matches } from "class-validator";

export class SignupDto {
    @Length(4, 16, {
        message: 'ID는 4~16글자 이내여야 합니다!'
    })
    @IsAlphanumeric('en-US', {
        message: 'ID는 영문자 또는 숫자여야 합니다!'
    })
    accountId: string;

    @Matches(/^[A-Za-z0-9!@#$%^&*+=-]{8,32}$/, {
        message: '패스워드는 영문자, 숫자, 특수문자로 8~32글자 이내여야 합니다!'
    })
    password: string;

    @Length(1, 16, {
        message: '닉네임은 1~16글자 이내여야 합니다!'
    })
    nickname: string;
}