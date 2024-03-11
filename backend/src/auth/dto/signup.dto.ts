import { Member } from "src/db/entity/member";

export class SignupDto {
    accountId: string;

    password: string;

    nickname: string;
}