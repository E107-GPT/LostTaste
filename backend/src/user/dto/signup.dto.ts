import { IsAlphanumeric, Length, Matches } from "class-validator";

export class SignupDto {
    @Length(4, 16)
    @IsAlphanumeric()
    accountId: string;

    @Matches(/^[A-Za-z0-9!@#$%^&*+=-]{8,32}$/)
    password: string;

    @Length(1, 16)
    nickname: string;
}