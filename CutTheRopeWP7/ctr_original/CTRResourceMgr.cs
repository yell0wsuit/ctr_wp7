using System;
using System.Collections.Generic;

using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.ios;

namespace ctr_wp7.ctr_original
{
    // Token: 0x020000B9 RID: 185
    internal class CTRResourceMgr : ResourceMgr
    {
        // Token: 0x0600053C RID: 1340 RVA: 0x0002644D File Offset: 0x0002464D
        public override NSObject init()
        {
            base.init();
            return this;
        }

        // Token: 0x0600053D RID: 1341 RVA: 0x00026457 File Offset: 0x00024657
        public static int handleResource(int r)
        {
            return CTRResourceMgr.handleLocalizedResource(CTRResourceMgr.handleWvgaResource(r));
        }

        // Token: 0x0600053E RID: 1342 RVA: 0x00026464 File Offset: 0x00024664
        public override float getNormalScaleX(int r)
        {
            return 1f;
        }

        // Token: 0x0600053F RID: 1343 RVA: 0x0002646B File Offset: 0x0002466B
        public override float getNormalScaleY(int r)
        {
            return 1f;
        }

        // Token: 0x06000540 RID: 1344 RVA: 0x00026472 File Offset: 0x00024672
        public override float getWvgaScaleX(int r)
        {
            return 1.5f;
        }

        // Token: 0x06000541 RID: 1345 RVA: 0x0002647C File Offset: 0x0002467C
        public override float getWvgaScaleY(int r)
        {
            if (r != 79)
            {
                switch (r)
                {
                    case 230:
                    case 232:
                    case 234:
                    case 236:
                    case 238:
                    case 240:
                    case 242:
                    case 244:
                    case 246:
                    case 248:
                    case 250:
                    case 252:
                    case 254:
                    case 256:
                        goto IL_00C6;
                    case 231:
                    case 233:
                    case 235:
                    case 237:
                    case 239:
                    case 241:
                    case 243:
                    case 245:
                    case 247:
                    case 249:
                    case 251:
                    case 253:
                    case 255:
                    case 257:
                        break;
                    case 258:
                    case 259:
                    case 260:
                    case 261:
                    case 262:
                    case 263:
                    case 264:
                    case 265:
                    case 266:
                    case 267:
                    case 268:
                    case 269:
                    case 270:
                    case 271:
                        return 1.65f;
                    default:
                        if (r == 408)
                        {
                            goto IL_00C6;
                        }
                        break;
                }
                return 1.5f;
            }
        IL_00C6:
            return 1.6666666f;
        }

        // Token: 0x06000542 RID: 1346 RVA: 0x00026560 File Offset: 0x00024760
        public override bool isWvgaResource(int r)
        {
            if (!FrameworkTypes.IS_WVGA)
            {
                return false;
            }
            switch (r)
            {
                case 2:
                case 3:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                    break;
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                    return false;
                default:
                    switch (r)
                    {
                        case 79:
                        case 80:
                        case 81:
                        case 82:
                        case 83:
                        case 84:
                        case 85:
                        case 86:
                        case 87:
                        case 88:
                        case 89:
                        case 90:
                        case 91:
                        case 105:
                        case 106:
                        case 107:
                        case 108:
                        case 109:
                        case 110:
                        case 111:
                        case 112:
                        case 113:
                        case 114:
                        case 115:
                        case 116:
                        case 117:
                        case 118:
                        case 150:
                        case 151:
                        case 152:
                        case 153:
                        case 154:
                        case 155:
                        case 156:
                        case 157:
                        case 158:
                        case 159:
                        case 160:
                        case 161:
                        case 162:
                        case 163:
                        case 164:
                        case 165:
                        case 166:
                        case 167:
                        case 168:
                        case 169:
                        case 170:
                        case 171:
                        case 172:
                        case 173:
                        case 174:
                        case 175:
                        case 176:
                        case 177:
                        case 178:
                        case 179:
                        case 181:
                        case 183:
                        case 185:
                        case 187:
                        case 230:
                        case 231:
                        case 232:
                        case 233:
                        case 234:
                        case 235:
                        case 236:
                        case 237:
                        case 238:
                        case 239:
                        case 240:
                        case 241:
                        case 242:
                        case 243:
                        case 244:
                        case 245:
                        case 246:
                        case 247:
                        case 248:
                        case 249:
                        case 250:
                        case 251:
                        case 252:
                        case 253:
                        case 254:
                        case 255:
                        case 256:
                        case 257:
                        case 258:
                        case 259:
                        case 260:
                        case 261:
                        case 262:
                        case 263:
                        case 264:
                        case 265:
                        case 266:
                        case 267:
                        case 268:
                        case 269:
                        case 270:
                        case 271:
                        case 286:
                        case 287:
                        case 288:
                        case 289:
                        case 290:
                        case 291:
                        case 292:
                        case 293:
                        case 294:
                        case 295:
                        case 296:
                        case 297:
                        case 298:
                        case 299:
                        case 313:
                        case 314:
                        case 315:
                        case 316:
                        case 317:
                        case 318:
                        case 319:
                        case 320:
                        case 321:
                        case 322:
                        case 323:
                        case 324:
                        case 325:
                        case 345:
                        case 346:
                        case 347:
                        case 348:
                        case 349:
                        case 350:
                        case 351:
                        case 352:
                        case 353:
                        case 354:
                        case 355:
                        case 356:
                        case 357:
                        case 358:
                        case 359:
                        case 360:
                        case 361:
                        case 362:
                        case 363:
                        case 376:
                        case 377:
                        case 378:
                        case 379:
                        case 380:
                        case 381:
                        case 382:
                        case 383:
                        case 384:
                        case 385:
                        case 386:
                        case 387:
                        case 388:
                        case 393:
                        case 394:
                        case 395:
                        case 396:
                        case 398:
                        case 400:
                        case 404:
                        case 405:
                        case 406:
                        case 408:
                        case 410:
                            break;
                        case 92:
                        case 93:
                        case 94:
                        case 95:
                        case 96:
                        case 97:
                        case 98:
                        case 99:
                        case 100:
                        case 101:
                        case 102:
                        case 103:
                        case 104:
                        case 119:
                        case 120:
                        case 121:
                        case 122:
                        case 123:
                        case 124:
                        case 125:
                        case 126:
                        case 127:
                        case 128:
                        case 129:
                        case 130:
                        case 131:
                        case 132:
                        case 133:
                        case 134:
                        case 135:
                        case 136:
                        case 137:
                        case 138:
                        case 139:
                        case 140:
                        case 141:
                        case 142:
                        case 143:
                        case 144:
                        case 145:
                        case 146:
                        case 147:
                        case 148:
                        case 149:
                        case 180:
                        case 182:
                        case 184:
                        case 186:
                        case 188:
                        case 189:
                        case 190:
                        case 191:
                        case 192:
                        case 193:
                        case 194:
                        case 195:
                        case 196:
                        case 197:
                        case 198:
                        case 199:
                        case 200:
                        case 201:
                        case 202:
                        case 203:
                        case 204:
                        case 205:
                        case 206:
                        case 207:
                        case 208:
                        case 209:
                        case 210:
                        case 211:
                        case 212:
                        case 213:
                        case 214:
                        case 215:
                        case 216:
                        case 217:
                        case 218:
                        case 219:
                        case 220:
                        case 221:
                        case 222:
                        case 223:
                        case 224:
                        case 225:
                        case 226:
                        case 227:
                        case 228:
                        case 229:
                        case 272:
                        case 273:
                        case 274:
                        case 275:
                        case 276:
                        case 277:
                        case 278:
                        case 279:
                        case 280:
                        case 281:
                        case 282:
                        case 283:
                        case 284:
                        case 285:
                        case 300:
                        case 301:
                        case 302:
                        case 303:
                        case 304:
                        case 305:
                        case 306:
                        case 307:
                        case 308:
                        case 309:
                        case 310:
                        case 311:
                        case 312:
                        case 326:
                        case 327:
                        case 328:
                        case 329:
                        case 330:
                        case 331:
                        case 332:
                        case 333:
                        case 334:
                        case 335:
                        case 336:
                        case 337:
                        case 338:
                        case 339:
                        case 340:
                        case 341:
                        case 342:
                        case 343:
                        case 344:
                        case 364:
                        case 365:
                        case 366:
                        case 367:
                        case 368:
                        case 369:
                        case 370:
                        case 371:
                        case 372:
                        case 373:
                        case 374:
                        case 375:
                        case 389:
                        case 390:
                        case 391:
                        case 392:
                        case 397:
                        case 399:
                        case 401:
                        case 402:
                        case 403:
                        case 407:
                        case 409:
                            return false;
                        default:
                            return false;
                    }
                    break;
            }
            return true;
        }

        // Token: 0x06000543 RID: 1347 RVA: 0x00026B08 File Offset: 0x00024D08
        public static int handleWvgaResource(int r)
        {
            if (!FrameworkTypes.IS_WVGA)
            {
                return r;
            }
            switch (r)
            {
                case 0:
                    return 2;
                case 1:
                    return 3;
                case 2:
                case 3:
                    break;
                case 4:
                    return 12;
                case 5:
                    return 13;
                case 6:
                    return 14;
                case 7:
                    return 15;
                case 8:
                    return 16;
                case 9:
                    return 17;
                case 10:
                    return 18;
                case 11:
                    return 19;
                default:
                    switch (r)
                    {
                        case 66:
                            return 79;
                        case 67:
                            return 80;
                        case 68:
                            return 81;
                        case 69:
                            return 82;
                        case 70:
                            return 83;
                        case 71:
                            return 84;
                        case 72:
                            return 85;
                        case 73:
                            return 86;
                        case 74:
                            return 87;
                        case 75:
                            return 88;
                        case 76:
                            return 89;
                        case 77:
                            return 90;
                        case 78:
                            return 91;
                        case 79:
                        case 80:
                        case 81:
                        case 82:
                        case 83:
                        case 84:
                        case 85:
                        case 86:
                        case 87:
                        case 88:
                        case 89:
                        case 90:
                        case 91:
                        case 105:
                        case 106:
                        case 107:
                        case 108:
                        case 109:
                        case 110:
                        case 111:
                        case 112:
                        case 113:
                        case 114:
                        case 115:
                        case 116:
                        case 117:
                        case 118:
                        case 150:
                        case 151:
                        case 152:
                        case 153:
                        case 154:
                        case 155:
                        case 156:
                        case 157:
                        case 158:
                        case 159:
                        case 160:
                        case 161:
                        case 162:
                        case 163:
                        case 164:
                        case 165:
                        case 166:
                        case 167:
                        case 168:
                        case 169:
                        case 170:
                        case 171:
                        case 172:
                        case 173:
                        case 174:
                        case 175:
                        case 176:
                        case 177:
                        case 178:
                        case 179:
                        case 181:
                        case 183:
                        case 185:
                        case 187:
                        case 230:
                        case 231:
                        case 232:
                        case 233:
                        case 234:
                        case 235:
                        case 236:
                        case 237:
                        case 238:
                        case 239:
                        case 240:
                        case 241:
                        case 242:
                        case 243:
                        case 244:
                        case 245:
                        case 246:
                        case 247:
                        case 248:
                        case 249:
                        case 250:
                        case 251:
                        case 252:
                        case 253:
                        case 254:
                        case 255:
                        case 256:
                        case 257:
                        case 258:
                        case 259:
                        case 260:
                        case 261:
                        case 262:
                        case 263:
                        case 264:
                        case 265:
                        case 266:
                        case 267:
                        case 268:
                        case 269:
                        case 270:
                        case 271:
                        case 286:
                        case 287:
                        case 288:
                        case 289:
                        case 290:
                        case 291:
                        case 292:
                        case 293:
                        case 294:
                        case 295:
                        case 296:
                        case 297:
                        case 298:
                        case 299:
                        case 313:
                        case 314:
                        case 315:
                        case 316:
                        case 317:
                        case 318:
                        case 319:
                        case 320:
                        case 321:
                        case 322:
                        case 323:
                        case 324:
                        case 325:
                        case 345:
                        case 346:
                        case 347:
                        case 348:
                        case 349:
                        case 350:
                        case 351:
                        case 352:
                        case 353:
                        case 354:
                        case 355:
                        case 356:
                        case 357:
                        case 358:
                        case 359:
                        case 360:
                        case 361:
                        case 362:
                        case 363:
                            break;
                        case 92:
                            return 105;
                        case 93:
                            return 108;
                        case 94:
                            return 109;
                        case 95:
                            return 111;
                        case 96:
                            return 106;
                        case 97:
                            return 107;
                        case 98:
                            return 112;
                        case 99:
                            return 114;
                        case 100:
                            return 113;
                        case 101:
                            return 115;
                        case 102:
                            return 116;
                        case 103:
                            return 117;
                        case 104:
                            return 118;
                        case 119:
                            return 172;
                        case 120:
                            return 154;
                        case 121:
                            return 155;
                        case 122:
                            return 164;
                        case 123:
                            return 171;
                        case 124:
                            return 153;
                        case 125:
                            return 162;
                        case 126:
                            return 163;
                        case 127:
                            return 173;
                        case 128:
                            return 152;
                        case 129:
                            return 170;
                        case 130:
                            return 169;
                        case 131:
                            return 168;
                        case 132:
                            return 150;
                        case 133:
                            return 151;
                        case 134:
                            return 166;
                        case 135:
                            return 156;
                        case 136:
                            return 157;
                        case 137:
                            return 158;
                        case 138:
                            return 159;
                        case 139:
                            return 160;
                        case 140:
                            return 161;
                        case 141:
                            return 110;
                        case 142:
                            return 165;
                        case 143:
                            return 167;
                        case 144:
                            return 174;
                        case 145:
                            return 175;
                        case 146:
                            return 176;
                        case 147:
                            return 177;
                        case 148:
                            return 178;
                        case 149:
                            return 179;
                        case 180:
                            return 181;
                        case 182:
                            return 183;
                        case 184:
                            return 185;
                        case 186:
                            return 187;
                        case 188:
                            return 230;
                        case 189:
                            return 231;
                        case 190:
                            return 232;
                        case 191:
                            return 233;
                        case 192:
                            return 234;
                        case 193:
                            return 235;
                        case 194:
                            return 236;
                        case 195:
                            return 237;
                        case 196:
                            return 238;
                        case 197:
                            return 239;
                        case 198:
                            return 240;
                        case 199:
                            return 241;
                        case 200:
                            return 242;
                        case 201:
                            return 243;
                        case 202:
                            return 244;
                        case 203:
                            return 245;
                        case 204:
                            return 246;
                        case 205:
                            return 247;
                        case 206:
                            return 248;
                        case 207:
                            return 249;
                        case 208:
                            return 250;
                        case 209:
                            return 251;
                        case 210:
                            return 252;
                        case 211:
                            return 253;
                        case 212:
                            return 254;
                        case 213:
                            return 255;
                        case 214:
                            return 256;
                        case 215:
                            return 257;
                        case 216:
                            return 258;
                        case 217:
                            return 259;
                        case 218:
                            return 260;
                        case 219:
                            return 261;
                        case 220:
                            return 262;
                        case 221:
                            return 263;
                        case 222:
                            return 264;
                        case 223:
                            return 265;
                        case 224:
                            return 266;
                        case 225:
                            return 267;
                        case 226:
                            return 268;
                        case 227:
                            return 269;
                        case 228:
                            return 270;
                        case 229:
                            return 271;
                        case 272:
                            return 288;
                        case 273:
                            return 287;
                        case 274:
                            return 286;
                        case 275:
                            return 289;
                        case 276:
                            return 290;
                        case 277:
                            return 297;
                        case 278:
                            return 296;
                        case 279:
                            return 295;
                        case 280:
                            return 298;
                        case 281:
                            return 299;
                        case 282:
                            return 292;
                        case 283:
                            return 291;
                        case 284:
                            return 293;
                        case 285:
                            return 294;
                        case 300:
                            return 313;
                        case 301:
                            return 314;
                        case 302:
                            return 315;
                        case 303:
                            return 316;
                        case 304:
                            return 317;
                        case 305:
                            return 318;
                        case 306:
                            return 319;
                        case 307:
                            return 320;
                        case 308:
                            return 321;
                        case 309:
                            return 322;
                        case 310:
                            return 323;
                        case 311:
                            return 324;
                        case 312:
                            return 325;
                        case 326:
                            return 345;
                        case 327:
                            return 346;
                        case 328:
                            return 347;
                        case 329:
                            return 348;
                        case 330:
                            return 349;
                        case 331:
                            return 350;
                        case 332:
                            return 351;
                        case 333:
                            return 352;
                        case 334:
                            return 353;
                        case 335:
                            return 354;
                        case 336:
                            return 355;
                        case 337:
                            return 356;
                        case 338:
                            return 357;
                        case 339:
                            return 358;
                        case 340:
                            return 359;
                        case 341:
                            return 360;
                        case 342:
                            return 361;
                        case 343:
                            return 362;
                        case 344:
                            return 363;
                        case 364:
                            return 376;
                        case 365:
                            return 377;
                        case 366:
                            return 378;
                        case 367:
                            return 379;
                        case 368:
                            return 380;
                        case 369:
                            return 381;
                        case 370:
                            return 382;
                        case 371:
                            return 383;
                        case 372:
                            return 384;
                        case 373:
                            return 385;
                        case 374:
                            return 386;
                        case 375:
                            return 387;
                        default:
                            switch (r)
                            {
                                case 397:
                                    return 398;
                                case 399:
                                    return 400;
                                case 401:
                                    return 404;
                                case 402:
                                    return 405;
                                case 403:
                                    return 406;
                                case 407:
                                    return 408;
                                case 409:
                                    return 410;
                            }
                            break;
                    }
                    break;
            }
            return r;
        }

        // Token: 0x06000544 RID: 1348 RVA: 0x00027438 File Offset: 0x00025638
        public static int handleLocalizedResource(int r)
        {
            if (r <= 86)
            {
                if (r != 73)
                {
                    if (r == 86)
                    {
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_RU)
                        {
                            return 288;
                        }
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_DE)
                        {
                            return 287;
                        }
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_FR)
                        {
                            return 286;
                        }
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_ZH)
                        {
                            return 289;
                        }
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_JA)
                        {
                            return 290;
                        }
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_KO)
                        {
                            return 376;
                        }
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_ES)
                        {
                            return 377;
                        }
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_IT)
                        {
                            return 378;
                        }
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_NL)
                        {
                            return 379;
                        }
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_BR)
                        {
                            return 380;
                        }
                    }
                }
                else
                {
                    if (ResDataPhoneFull.LANGUAGE == Language.LANG_RU)
                    {
                        return 272;
                    }
                    if (ResDataPhoneFull.LANGUAGE == Language.LANG_DE)
                    {
                        return 273;
                    }
                    if (ResDataPhoneFull.LANGUAGE == Language.LANG_FR)
                    {
                        return 274;
                    }
                    if (ResDataPhoneFull.LANGUAGE == Language.LANG_ZH)
                    {
                        return 275;
                    }
                    if (ResDataPhoneFull.LANGUAGE == Language.LANG_JA)
                    {
                        return 276;
                    }
                    if (ResDataPhoneFull.LANGUAGE == Language.LANG_KO)
                    {
                        return 364;
                    }
                    if (ResDataPhoneFull.LANGUAGE == Language.LANG_ES)
                    {
                        return 365;
                    }
                    if (ResDataPhoneFull.LANGUAGE == Language.LANG_IT)
                    {
                        return 366;
                    }
                    if (ResDataPhoneFull.LANGUAGE == Language.LANG_NL)
                    {
                        return 367;
                    }
                    if (ResDataPhoneFull.LANGUAGE == Language.LANG_BR)
                    {
                        return 368;
                    }
                }
            }
            else
            {
                switch (r)
                {
                    case 99:
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_RU)
                        {
                            return 277;
                        }
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_DE)
                        {
                            return 278;
                        }
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_FR)
                        {
                            return 279;
                        }
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_ZH)
                        {
                            return 280;
                        }
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_JA)
                        {
                            return 281;
                        }
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_KO)
                        {
                            return 371;
                        }
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_ES)
                        {
                            return 372;
                        }
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_IT)
                        {
                            return 373;
                        }
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_NL)
                        {
                            return 374;
                        }
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_BR)
                        {
                            return 375;
                        }
                        break;
                    case 100:
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_RU)
                        {
                            return 282;
                        }
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_DE)
                        {
                            return 283;
                        }
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_FR)
                        {
                            return 100;
                        }
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_ZH)
                        {
                            return 284;
                        }
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_JA)
                        {
                            return 285;
                        }
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_KO)
                        {
                            return 369;
                        }
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_ES)
                        {
                            return 370;
                        }
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_IT)
                        {
                            return 100;
                        }
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_NL)
                        {
                            return 100;
                        }
                        if (ResDataPhoneFull.LANGUAGE == Language.LANG_BR)
                        {
                            return 100;
                        }
                        break;
                    default:
                        switch (r)
                        {
                            case 113:
                                if (ResDataPhoneFull.LANGUAGE == Language.LANG_RU)
                                {
                                    return 292;
                                }
                                if (ResDataPhoneFull.LANGUAGE == Language.LANG_DE)
                                {
                                    return 291;
                                }
                                if (ResDataPhoneFull.LANGUAGE == Language.LANG_FR)
                                {
                                    return 113;
                                }
                                if (ResDataPhoneFull.LANGUAGE == Language.LANG_ZH)
                                {
                                    return 293;
                                }
                                if (ResDataPhoneFull.LANGUAGE == Language.LANG_JA)
                                {
                                    return 294;
                                }
                                if (ResDataPhoneFull.LANGUAGE == Language.LANG_KO)
                                {
                                    return 381;
                                }
                                if (ResDataPhoneFull.LANGUAGE == Language.LANG_ES)
                                {
                                    return 382;
                                }
                                if (ResDataPhoneFull.LANGUAGE == Language.LANG_IT)
                                {
                                    return 113;
                                }
                                if (ResDataPhoneFull.LANGUAGE == Language.LANG_NL)
                                {
                                    return 113;
                                }
                                if (ResDataPhoneFull.LANGUAGE == Language.LANG_BR)
                                {
                                    return 113;
                                }
                                break;
                            case 114:
                                if (ResDataPhoneFull.LANGUAGE == Language.LANG_RU)
                                {
                                    return 297;
                                }
                                if (ResDataPhoneFull.LANGUAGE == Language.LANG_DE)
                                {
                                    return 296;
                                }
                                if (ResDataPhoneFull.LANGUAGE == Language.LANG_FR)
                                {
                                    return 295;
                                }
                                if (ResDataPhoneFull.LANGUAGE == Language.LANG_ZH)
                                {
                                    return 298;
                                }
                                if (ResDataPhoneFull.LANGUAGE == Language.LANG_JA)
                                {
                                    return 299;
                                }
                                if (ResDataPhoneFull.LANGUAGE == Language.LANG_KO)
                                {
                                    return 383;
                                }
                                if (ResDataPhoneFull.LANGUAGE == Language.LANG_ES)
                                {
                                    return 384;
                                }
                                if (ResDataPhoneFull.LANGUAGE == Language.LANG_IT)
                                {
                                    return 385;
                                }
                                if (ResDataPhoneFull.LANGUAGE == Language.LANG_NL)
                                {
                                    return 386;
                                }
                                if (ResDataPhoneFull.LANGUAGE == Language.LANG_BR)
                                {
                                    return 387;
                                }
                                break;
                        }
                        break;
                }
            }
            return r;
        }

        // Token: 0x06000545 RID: 1349 RVA: 0x000277D4 File Offset: 0x000259D4
        public static string XNA_ResName(int resId)
        {
            if (CTRResourceMgr.resNames_ == null)
            {
                Dictionary<int, string> dictionary = new Dictionary<int, string>();
                dictionary.Add(0, "zeptolab");
                dictionary.Add(1, "loaderbar_full");
                dictionary.Add(2, "zeptolab_hd");
                dictionary.Add(3, "loaderbar_full_hd");
                dictionary.Add(4, "menu_button_default");
                dictionary.Add(5, "big_font");
                dictionary.Add(6, "small_font");
                dictionary.Add(7, "menu_loading");
                dictionary.Add(8, "drawing_canvas");
                dictionary.Add(9, "menu_drawings_bigpage_markers");
                dictionary.Add(10, "menu_button_short");
                dictionary.Add(11, "drawings_particles");
                dictionary.Add(12, "menu_button_default_hd");
                dictionary.Add(13, "big_font_hd");
                dictionary.Add(14, "small_font_hd");
                dictionary.Add(15, "menu_loading_hd");
                dictionary.Add(16, "drawing_canvas_hd");
                dictionary.Add(17, "menu_drawings_bigpage_markers_hd");
                dictionary.Add(18, "menu_button_short_hd");
                dictionary.Add(19, "drawings_particles_hd");
                dictionary.Add(20, "menu_strings");
                dictionary.Add(21, "tap");
                dictionary.Add(22, "bubble_break");
                dictionary.Add(23, "bubble");
                dictionary.Add(24, "candy_break");
                dictionary.Add(25, "monster_chewing");
                dictionary.Add(26, "monster_close");
                dictionary.Add(27, "monster_open");
                dictionary.Add(28, "monster_sad");
                dictionary.Add(29, "rope_bleak_1");
                dictionary.Add(30, "rope_bleak_2");
                dictionary.Add(31, "rope_bleak_3");
                dictionary.Add(32, "rope_bleak_4");
                dictionary.Add(33, "rope_get");
                dictionary.Add(34, "scrape");
                dictionary.Add(35, "star_1");
                dictionary.Add(36, "star_2");
                dictionary.Add(37, "star_3");
                dictionary.Add(38, "electric");
                dictionary.Add(39, "pump_1");
                dictionary.Add(40, "pump_2");
                dictionary.Add(41, "pump_3");
                dictionary.Add(42, "pump_4");
                dictionary.Add(60, "ghost_puff");
                dictionary.Add(61, "steam_start");
                dictionary.Add(62, "steam_start_2");
                dictionary.Add(63, "steam_end");
                dictionary.Add(43, "spider_activate");
                dictionary.Add(44, "spider_fall");
                dictionary.Add(45, "spider_win");
                dictionary.Add(46, "wheel");
                dictionary.Add(47, "win");
                dictionary.Add(48, "gravity_off");
                dictionary.Add(49, "gravity_on");
                dictionary.Add(50, "candy_link");
                dictionary.Add(51, "teleport");
                dictionary.Add(52, "bouncer");
                dictionary.Add(53, "spike_rotate_in");
                dictionary.Add(54, "spike_rotate_out");
                dictionary.Add(55, "buzz");
                dictionary.Add(56, "scratch_in");
                dictionary.Add(57, "scratch_out");
                dictionary.Add(64, "lantern_teleport_in");
                dictionary.Add(65, "lantern_teleport_out");
                dictionary.Add(66, "menu_bgr");
                dictionary.Add(67, "menu_button_crystal");
                dictionary.Add(68, "menu_popup");
                dictionary.Add(69, "menu_logo");
                dictionary.Add(70, "menu_level_selection");
                dictionary.Add(71, "menu_pack_selection");
                dictionary.Add(399, "menu_pack_selection_boxes");
                dictionary.Add(401, "menu_content_screen");
                dictionary.Add(402, "menu_player_afterplay");
                dictionary.Add(403, "menu_player");
                dictionary.Add(72, "menu_extra_buttons");
                dictionary.Add(73, "menu_extra_buttons_en");
                dictionary.Add(74, "menu_shadow");
                dictionary.Add(75, "candy_select_fx_demo");
                dictionary.Add(76, "menu_processing");
                dictionary.Add(77, "menu_promo");
                dictionary.Add(78, "menu_promo_banner");
                dictionary.Add(79, "menu_bgr_hd");
                dictionary.Add(80, "menu_button_crystal_hd");
                dictionary.Add(81, "menu_popup_hd");
                dictionary.Add(82, "menu_logo_hd");
                dictionary.Add(83, "menu_level_selection_hd");
                dictionary.Add(84, "menu_pack_selection_hd");
                dictionary.Add(400, "menu_pack_selection_boxes_hd");
                dictionary.Add(404, "menu_content_screen_hd");
                dictionary.Add(405, "menu_player_afterplay_hd");
                dictionary.Add(406, "menu_player_hd");
                dictionary.Add(85, "menu_extra_buttons_hd");
                dictionary.Add(86, "menu_extra_buttons_en_hd");
                dictionary.Add(87, "menu_shadow_hd");
                dictionary.Add(88, "candy_select_fx_demo_hd");
                dictionary.Add(89, "menu_processing_hd");
                dictionary.Add(90, "menu_promo_hd");
                dictionary.Add(91, "menu_promo_banner_hd");
                dictionary.Add(92, "hud_buttons");
                dictionary.Add(93, "obj_candy_01");
                dictionary.Add(94, "obj_spider");
                dictionary.Add(95, "confetti_particles");
                dictionary.Add(96, "menu_pause");
                dictionary.Add(97, "menu_result");
                dictionary.Add(98, "font_numbers_big");
                dictionary.Add(99, "menu_result_en");
                dictionary.Add(100, "hud_buttons_en");
                dictionary.Add(101, "obj_candy_02");
                dictionary.Add(102, "obj_candy_03");
                dictionary.Add(103, "obj_candy_fx");
                dictionary.Add(104, "drawing_hidden");
                dictionary.Add(105, "hud_buttons_hd");
                dictionary.Add(106, "menu_pause_hd");
                dictionary.Add(107, "menu_result_hd");
                dictionary.Add(108, "obj_candy_01_hd");
                dictionary.Add(109, "obj_spider_hd");
                dictionary.Add(110, "obj_vinil_hd");
                dictionary.Add(111, "confetti_particles_hd");
                dictionary.Add(112, "font_numbers_big_hd");
                dictionary.Add(113, "hud_buttons_en_hd");
                dictionary.Add(114, "menu_result_en_hd");
                dictionary.Add(115, "obj_candy_02_hd");
                dictionary.Add(116, "obj_candy_03_hd");
                dictionary.Add(117, "obj_candy_fx_hd");
                dictionary.Add(118, "drawing_hidden_hd");
                dictionary.Add(119, "obj_star_disappear");
                dictionary.Add(120, "obj_bubble_flight");
                dictionary.Add(121, "obj_bubble_pop");
                dictionary.Add(122, "obj_hook_auto");
                dictionary.Add(123, "obj_spikes_04");
                dictionary.Add(124, "obj_bubble_attached");
                dictionary.Add(125, "obj_hook_01");
                dictionary.Add(126, "obj_hook_02");
                dictionary.Add(127, "obj_star_idle");
                dictionary.Add(128, "hud_star");
                dictionary.Add(129, "obj_spikes_03");
                dictionary.Add(130, "obj_spikes_02");
                dictionary.Add(131, "obj_spikes_01");
                dictionary.Add(132, "char_animations");
                dictionary.Add(133, "char_animations_idle");
                dictionary.Add(134, "obj_hook_regulated");
                dictionary.Add(135, "obj_electrodes");
                dictionary.Add(136, "obj_rotatable_spikes_01");
                dictionary.Add(137, "obj_rotatable_spikes_02");
                dictionary.Add(138, "obj_rotatable_spikes_03");
                dictionary.Add(139, "obj_rotatable_spikes_04");
                dictionary.Add(140, "obj_rotatable_spikes_button");
                dictionary.Add(141, "obj_vinil");
                dictionary.Add(142, "obj_hook_movable");
                dictionary.Add(143, "obj_pump");
                dictionary.Add(144, "tutorial_signs");
                dictionary.Add(145, "obj_hat");
                dictionary.Add(146, "obj_bouncer_01");
                dictionary.Add(147, "obj_bouncer_02");
                dictionary.Add(148, "obj_bee");
                dictionary.Add(149, "obj_pollen");
                dictionary.Add(150, "char_animations_hd");
                dictionary.Add(151, "char_animations_idle_hd");
                dictionary.Add(152, "hud_star_hd");
                dictionary.Add(153, "obj_bubble_attached_hd");
                dictionary.Add(154, "obj_bubble_flight_hd");
                dictionary.Add(155, "obj_bubble_pop_hd");
                dictionary.Add(156, "obj_electrodes_hd");
                dictionary.Add(157, "obj_rotatable_spikes_01_hd");
                dictionary.Add(158, "obj_rotatable_spikes_02_hd");
                dictionary.Add(159, "obj_rotatable_spikes_03_hd");
                dictionary.Add(160, "obj_rotatable_spikes_04_hd");
                dictionary.Add(161, "obj_rotatable_spikes_button_hd");
                dictionary.Add(162, "obj_hook_01_hd");
                dictionary.Add(163, "obj_hook_02_hd");
                dictionary.Add(164, "obj_hook_auto_hd");
                dictionary.Add(165, "obj_hook_movable_hd");
                dictionary.Add(166, "obj_hook_regulated_hd");
                dictionary.Add(167, "obj_pump_hd");
                dictionary.Add(168, "obj_spikes_01_hd");
                dictionary.Add(169, "obj_spikes_02_hd");
                dictionary.Add(170, "obj_spikes_03_hd");
                dictionary.Add(171, "obj_spikes_04_hd");
                dictionary.Add(172, "obj_star_disappear_hd");
                dictionary.Add(173, "obj_star_idle_hd");
                dictionary.Add(174, "tutorial_signs_hd");
                dictionary.Add(175, "obj_hat_hd");
                dictionary.Add(176, "obj_bouncer_01_hd");
                dictionary.Add(177, "obj_bouncer_02_hd");
                dictionary.Add(178, "obj_bee_hd");
                dictionary.Add(179, "obj_pollen_hd");
                dictionary.Add(188, "bgr_01");
                dictionary.Add(189, "char_support_01");
                dictionary.Add(190, "bgr_02");
                dictionary.Add(191, "char_support_02");
                dictionary.Add(192, "bgr_03");
                dictionary.Add(193, "char_support_03");
                dictionary.Add(194, "bgr_04");
                dictionary.Add(195, "char_support_04");
                dictionary.Add(196, "bgr_05");
                dictionary.Add(197, "char_support_05");
                dictionary.Add(198, "bgr_06");
                dictionary.Add(199, "char_support_06");
                dictionary.Add(200, "bgr_07");
                dictionary.Add(201, "char_support_07");
                dictionary.Add(202, "bgr_08");
                dictionary.Add(203, "char_support_08");
                dictionary.Add(204, "bgr_09");
                dictionary.Add(205, "char_support_09");
                dictionary.Add(206, "bgr_10");
                dictionary.Add(207, "char_support_10");
                dictionary.Add(208, "bgr_11");
                dictionary.Add(209, "char_support_11");
                dictionary.Add(210, "bgr_12");
                dictionary.Add(211, "char_support_12");
                dictionary.Add(212, "bgr_13");
                dictionary.Add(213, "char_support_13");
                dictionary.Add(214, "bgr_14");
                dictionary.Add(215, "char_support_14");
                dictionary.Add(216, "bgr_01_cover");
                dictionary.Add(217, "bgr_02_cover");
                dictionary.Add(218, "bgr_03_cover");
                dictionary.Add(219, "bgr_04_cover");
                dictionary.Add(220, "bgr_05_cover");
                dictionary.Add(221, "bgr_06_cover");
                dictionary.Add(222, "bgr_07_cover");
                dictionary.Add(223, "bgr_08_cover");
                dictionary.Add(224, "bgr_09_cover");
                dictionary.Add(225, "bgr_10_cover");
                dictionary.Add(226, "bgr_11_cover");
                dictionary.Add(227, "bgr_12_cover");
                dictionary.Add(228, "bgr_13_cover");
                dictionary.Add(229, "bgr_14_cover");
                dictionary.Add(230, "bgr_01_hd");
                dictionary.Add(231, "char_support_01_hd");
                dictionary.Add(232, "bgr_02_hd");
                dictionary.Add(233, "char_support_02_hd");
                dictionary.Add(234, "bgr_03_hd");
                dictionary.Add(235, "char_support_03_hd");
                dictionary.Add(236, "bgr_04_hd");
                dictionary.Add(237, "char_support_04_hd");
                dictionary.Add(238, "bgr_05_hd");
                dictionary.Add(239, "char_support_05_hd");
                dictionary.Add(240, "bgr_06_hd");
                dictionary.Add(241, "char_support_06_hd");
                dictionary.Add(242, "bgr_07_hd");
                dictionary.Add(243, "char_support_07_hd");
                dictionary.Add(244, "bgr_08_hd");
                dictionary.Add(245, "char_support_08_hd");
                dictionary.Add(246, "bgr_09_hd");
                dictionary.Add(247, "char_support_09_hd");
                dictionary.Add(248, "bgr_10_hd");
                dictionary.Add(249, "char_support_10_hd");
                dictionary.Add(250, "bgr_11_hd");
                dictionary.Add(251, "char_support_11_hd");
                dictionary.Add(252, "bgr_12_hd");
                dictionary.Add(253, "char_support_12_hd");
                dictionary.Add(254, "bgr_13_hd");
                dictionary.Add(255, "char_support_13_hd");
                dictionary.Add(256, "bgr_14_hd");
                dictionary.Add(257, "char_support_14_hd");
                dictionary.Add(258, "bgr_01_cover_hd");
                dictionary.Add(259, "bgr_02_cover_hd");
                dictionary.Add(260, "bgr_03_cover_hd");
                dictionary.Add(261, "bgr_04_cover_hd");
                dictionary.Add(262, "bgr_05_cover_hd");
                dictionary.Add(263, "bgr_06_cover_hd");
                dictionary.Add(264, "bgr_07_cover_hd");
                dictionary.Add(265, "bgr_08_cover_hd");
                dictionary.Add(266, "bgr_09_cover_hd");
                dictionary.Add(267, "bgr_10_cover_hd");
                dictionary.Add(268, "bgr_11_cover_hd");
                dictionary.Add(269, "bgr_12_cover_hd");
                dictionary.Add(270, "bgr_13_cover_hd");
                dictionary.Add(271, "bgr_14_cover_hd");
                dictionary.Add(58, "menu_music");
                dictionary.Add(59, "game_music");
                dictionary.Add(272, "menu_extra_buttons_ru");
                dictionary.Add(273, "menu_extra_buttons_gr");
                dictionary.Add(274, "menu_extra_buttons_fr");
                dictionary.Add(275, "menu_extra_buttons_zh");
                dictionary.Add(276, "menu_extra_buttons_ja");
                dictionary.Add(364, "menu_extra_buttons_ko");
                dictionary.Add(365, "menu_extra_buttons_es");
                dictionary.Add(366, "menu_extra_buttons_it");
                dictionary.Add(367, "menu_extra_buttons_nl");
                dictionary.Add(368, "menu_extra_buttons_br");
                dictionary.Add(277, "menu_result_ru");
                dictionary.Add(278, "menu_result_gr");
                dictionary.Add(279, "menu_result_fr");
                dictionary.Add(280, "menu_result_zh");
                dictionary.Add(281, "menu_result_ja");
                dictionary.Add(371, "menu_result_ko");
                dictionary.Add(372, "menu_result_es");
                dictionary.Add(373, "menu_result_it");
                dictionary.Add(374, "menu_result_nl");
                dictionary.Add(375, "menu_result_br");
                dictionary.Add(282, "hud_buttons_ru");
                dictionary.Add(283, "hud_buttons_gr");
                dictionary.Add(284, "hud_buttons_zh");
                dictionary.Add(285, "hud_buttons_ja");
                dictionary.Add(369, "hud_buttons_ko");
                dictionary.Add(370, "hud_buttons_es");
                dictionary.Add(286, "menu_extra_buttons_fr_hd");
                dictionary.Add(287, "menu_extra_buttons_gr_hd");
                dictionary.Add(288, "menu_extra_buttons_ru_hd");
                dictionary.Add(289, "menu_extra_buttons_zh_hd");
                dictionary.Add(290, "menu_extra_buttons_ja_hd");
                dictionary.Add(376, "menu_extra_buttons_ko_hd");
                dictionary.Add(377, "menu_extra_buttons_es_hd");
                dictionary.Add(378, "menu_extra_buttons_it_hd");
                dictionary.Add(379, "menu_extra_buttons_nl_hd");
                dictionary.Add(380, "menu_extra_buttons_br_hd");
                dictionary.Add(291, "hud_buttons_gr_hd");
                dictionary.Add(292, "hud_buttons_ru_hd");
                dictionary.Add(293, "hud_buttons_zh_hd");
                dictionary.Add(294, "hud_buttons_ja_hd");
                dictionary.Add(381, "hud_buttons_ko_hd");
                dictionary.Add(382, "hud_buttons_es_hd");
                dictionary.Add(295, "menu_result_fr_hd");
                dictionary.Add(296, "menu_result_gr_hd");
                dictionary.Add(297, "menu_result_ru_hd");
                dictionary.Add(298, "menu_result_zh_hd");
                dictionary.Add(299, "menu_result_ja_hd");
                dictionary.Add(383, "menu_result_ko_hd");
                dictionary.Add(384, "menu_result_es_hd");
                dictionary.Add(385, "menu_result_it_hd");
                dictionary.Add(386, "menu_result_nl_hd");
                dictionary.Add(387, "menu_result_br_hd");
                dictionary.Add(300, "drawing_01");
                dictionary.Add(301, "drawing_02");
                dictionary.Add(302, "drawing_03");
                dictionary.Add(303, "drawing_04");
                dictionary.Add(304, "drawing_05");
                dictionary.Add(305, "drawing_06");
                dictionary.Add(306, "drawing_07");
                dictionary.Add(307, "drawing_08");
                dictionary.Add(308, "drawing_09");
                dictionary.Add(309, "drawing_10");
                dictionary.Add(310, "drawing_11");
                dictionary.Add(311, "drawing_12");
                dictionary.Add(312, "drawing_13");
                dictionary.Add(313, "drawing_01_hd");
                dictionary.Add(314, "drawing_02_hd");
                dictionary.Add(315, "drawing_03_hd");
                dictionary.Add(316, "drawing_04_hd");
                dictionary.Add(317, "drawing_05_hd");
                dictionary.Add(318, "drawing_06_hd");
                dictionary.Add(319, "drawing_07_hd");
                dictionary.Add(320, "drawing_08_hd");
                dictionary.Add(321, "drawing_09_hd");
                dictionary.Add(322, "drawing_10_hd");
                dictionary.Add(323, "drawing_11_hd");
                dictionary.Add(324, "drawing_12_hd");
                dictionary.Add(325, "drawing_13_hd");
                dictionary.Add(326, "menu_drawings_thumb_page");
                dictionary.Add(327, "drawing_canvas_locked");
                dictionary.Add(328, "drawing_facebook");
                dictionary.Add(329, "drawings_menu_markers");
                dictionary.Add(330, "drawings_thumb_01");
                dictionary.Add(331, "drawings_thumb_02");
                dictionary.Add(332, "drawings_thumb_03");
                dictionary.Add(333, "drawings_thumb_04");
                dictionary.Add(334, "drawings_thumb_05");
                dictionary.Add(335, "drawings_thumb_06");
                dictionary.Add(336, "drawings_thumb_07");
                dictionary.Add(337, "drawings_thumb_08");
                dictionary.Add(338, "drawings_thumb_09");
                dictionary.Add(339, "drawings_thumb_10");
                dictionary.Add(340, "drawings_thumb_11");
                dictionary.Add(341, "drawings_thumb_12");
                dictionary.Add(342, "drawings_thumb_13");
                dictionary.Add(343, "menu_drawings_bgr");
                dictionary.Add(344, "omnom_artist");
                dictionary.Add(345, "menu_drawings_thumb_page_hd");
                dictionary.Add(346, "drawing_canvas_locked_hd");
                dictionary.Add(347, "drawing_facebook_hd");
                dictionary.Add(348, "drawings_menu_markers_hd");
                dictionary.Add(349, "drawings_thumb_01_hd");
                dictionary.Add(350, "drawings_thumb_02_hd");
                dictionary.Add(351, "drawings_thumb_03_hd");
                dictionary.Add(352, "drawings_thumb_04_hd");
                dictionary.Add(353, "drawings_thumb_05_hd");
                dictionary.Add(354, "drawings_thumb_06_hd");
                dictionary.Add(355, "drawings_thumb_07_hd");
                dictionary.Add(356, "drawings_thumb_08_hd");
                dictionary.Add(357, "drawings_thumb_09_hd");
                dictionary.Add(358, "drawings_thumb_10_hd");
                dictionary.Add(359, "drawings_thumb_11_hd");
                dictionary.Add(360, "drawings_thumb_12_hd");
                dictionary.Add(361, "drawings_thumb_13_hd");
                dictionary.Add(362, "menu_drawings_bgr_hd");
                dictionary.Add(363, "omnom_artist_hd");
                dictionary.Add(388, "menu_scrollbar");
                dictionary.Add(389, "menu_button_achiv_cup");
                dictionary.Add(390, "menu_leaderboard");
                dictionary.Add(391, "empty_achievement");
                dictionary.Add(392, "arrows");
                dictionary.Add(393, "scotch_tape_1");
                dictionary.Add(394, "scotch_tape_2");
                dictionary.Add(395, "scotch_tape_3");
                dictionary.Add(396, "scotch_tape_4");
                dictionary.Add(397, "menu_audio_icons");
                dictionary.Add(398, "menu_audio_icons_hd");
                dictionary.Add(182, "obj_ghost");
                dictionary.Add(180, "obj_ghost_bubbles");
                dictionary.Add(183, "obj_ghost_hd");
                dictionary.Add(181, "obj_ghost_bubbles_hd");
                dictionary.Add(184, "obj_pipe");
                dictionary.Add(185, "obj_pipe_hd");
                dictionary.Add(187, "obj_lantern_hd");
                dictionary.Add(407, "menu_agepopup_bgr");
                dictionary.Add(408, "menu_agepopup_bgr_hd");
                dictionary.Add(409, "menu_agepopup");
                dictionary.Add(410, "menu_agepopup_hd");
                CTRResourceMgr.resNames_ = dictionary;
            }
            string text = null;
            CTRResourceMgr.resNames_.TryGetValue(CTRResourceMgr.handleLocalizedResource(CTRResourceMgr.handleWvgaResource(resId)), out text);
            return text;
        }

        // Token: 0x06000546 RID: 1350 RVA: 0x00029027 File Offset: 0x00027227
        public override NSObject loadResource(int resID, ResourceMgr.ResourceType resType)
        {
            return base.loadResource(CTRResourceMgr.handleLocalizedResource(CTRResourceMgr.handleWvgaResource(resID)), resType);
        }

        // Token: 0x06000547 RID: 1351 RVA: 0x0002903B File Offset: 0x0002723B
        public override void freeResource(int resID)
        {
            base.freeResource(CTRResourceMgr.handleLocalizedResource(CTRResourceMgr.handleWvgaResource(resID)));
        }

        // Token: 0x06000548 RID: 1352 RVA: 0x0002904E File Offset: 0x0002724E
        public override float getScaleX(int r)
        {
            return FrameworkTypes.CHOOSE3(1.0, 1.5, 2.5);
        }

        // Token: 0x06000549 RID: 1353 RVA: 0x00029070 File Offset: 0x00027270
        public override float getScaleY(int r)
        {
            if (r <= 79)
            {
                if (r != 66 && r != 79)
                {
                    goto IL_0173;
                }
            }
            else
            {
                switch (r)
                {
                    case 188:
                    case 190:
                    case 192:
                    case 194:
                    case 196:
                    case 198:
                    case 200:
                    case 202:
                    case 204:
                    case 206:
                    case 208:
                    case 210:
                    case 212:
                    case 214:
                    case 216:
                    case 217:
                    case 218:
                    case 219:
                    case 220:
                    case 221:
                    case 222:
                    case 223:
                    case 224:
                    case 225:
                    case 226:
                    case 227:
                    case 228:
                    case 229:
                    case 230:
                    case 232:
                    case 234:
                    case 236:
                    case 238:
                    case 240:
                    case 242:
                    case 244:
                    case 246:
                    case 248:
                    case 250:
                    case 252:
                    case 254:
                    case 256:
                        break;
                    case 189:
                    case 191:
                    case 193:
                    case 195:
                    case 197:
                    case 199:
                    case 201:
                    case 203:
                    case 205:
                    case 207:
                    case 209:
                    case 211:
                    case 213:
                    case 215:
                    case 231:
                    case 233:
                    case 235:
                    case 237:
                    case 239:
                    case 241:
                    case 243:
                    case 245:
                    case 247:
                    case 249:
                    case 251:
                    case 253:
                    case 255:
                        goto IL_0173;
                    default:
                        switch (r)
                        {
                            case 407:
                            case 408:
                                break;
                            default:
                                goto IL_0173;
                        }
                        break;
                }
            }
            return FrameworkTypes.CHOOSE3(1.0, 1.649999976158142, 2.6500000953674316);
        IL_0173:
            return FrameworkTypes.CHOOSE3(1.0, 1.5, 2.5);
        }

        // Token: 0x04000A8A RID: 2698
        private static Dictionary<int, string> resNames_;
    }
}
