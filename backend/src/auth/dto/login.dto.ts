import { IsAlphanumeric, IsNotEmpty, Length } from "class-validator";

export class LoginDto {
    @IsAlphanumeric()
    username: string;

    @IsNotEmpty()
    password: string;
}