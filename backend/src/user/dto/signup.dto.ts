import { Length } from "class-validator";

export class SignupDto {
    @Length(4, 16)
    accountId: string;

    @Length(4, 16)
    password: string;

    @Length(1, 16)
    nickname: string;
}