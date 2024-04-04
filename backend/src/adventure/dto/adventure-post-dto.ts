import { IsNotEmpty, Max, Min } from "class-validator";

export class AdventurePostDto {
    @IsNotEmpty({
        message: '파티 이름은 필수입니다!'
    })
    partyName: string

    @Min(1, {
        message: '파티 인원 수는 1명 이상이어야 합니다!'
    })
    @Max(4, {
        message: '파티 인원 수는 4명 이하여야 합니다!'
    })
    memberCount: number

    @Min(0, {
        message: '플레이 시간은 0초 이상이어야 합니다!'
    })
    playTimeSec: number

    @Min(-2_147_483_648, {
        message: 'RNG 시드값은 int32 범위 이내여야 합니다!'
    })
    @Max(+2_147_483_647, {
        message: 'RNG 시드값은 int32 범위 이내여야 합니다!'
    })
    rngSeed: number
}