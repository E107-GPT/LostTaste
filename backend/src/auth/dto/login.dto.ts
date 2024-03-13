import { IsAlphanumeric, IsNotEmpty, Length } from "class-validator";

export class LoginDto {
    @IsAlphanumeric()
    accountId: string;

    @IsNotEmpty()
    password: string;
}