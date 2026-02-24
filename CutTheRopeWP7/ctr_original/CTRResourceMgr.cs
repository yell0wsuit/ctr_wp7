using System.Collections.Generic;

using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.ios;

namespace ctr_wp7.ctr_original
{
    internal sealed class CTRResourceMgr : ResourceMgr
    {
        public override NSObject init()
        {
            _ = base.init();
            return this;
        }

        public static int handleResource(int r)
        {
            return handleLocalizedResource(handleWvgaResource(r));
        }

        public override float getNormalScaleX(int r)
        {
            return 1f;
        }

        public override float getNormalScaleY(int r)
        {
            return 1f;
        }

        public override float getWvgaScaleX(int r)
        {
            return 1.5f;
        }

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

        public override bool isWvgaResource(int r)
        {
            if (!IS_WVGA)
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

        public static int handleWvgaResource(int r)
        {
            if (!IS_WVGA)
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

        public static int handleLocalizedResource(int r)
        {
            if (r <= 86)
            {
                if (r != 73)
                {
                    if (r == 86)
                    {
                        if (LANGUAGE == Language.LANG_RU)
                        {
                            return 288;
                        }
                        if (LANGUAGE == Language.LANG_DE)
                        {
                            return 287;
                        }
                        if (LANGUAGE == Language.LANG_FR)
                        {
                            return 286;
                        }
                        if (LANGUAGE == Language.LANG_ZH)
                        {
                            return 289;
                        }
                        if (LANGUAGE == Language.LANG_JA)
                        {
                            return 290;
                        }
                        if (LANGUAGE == Language.LANG_KO)
                        {
                            return 376;
                        }
                        if (LANGUAGE == Language.LANG_ES)
                        {
                            return 377;
                        }
                        if (LANGUAGE == Language.LANG_IT)
                        {
                            return 378;
                        }
                        if (LANGUAGE == Language.LANG_NL)
                        {
                            return 379;
                        }
                        if (LANGUAGE == Language.LANG_BR)
                        {
                            return 380;
                        }
                    }
                }
                else
                {
                    if (LANGUAGE == Language.LANG_RU)
                    {
                        return 272;
                    }
                    if (LANGUAGE == Language.LANG_DE)
                    {
                        return 273;
                    }
                    if (LANGUAGE == Language.LANG_FR)
                    {
                        return 274;
                    }
                    if (LANGUAGE == Language.LANG_ZH)
                    {
                        return 275;
                    }
                    if (LANGUAGE == Language.LANG_JA)
                    {
                        return 276;
                    }
                    if (LANGUAGE == Language.LANG_KO)
                    {
                        return 364;
                    }
                    if (LANGUAGE == Language.LANG_ES)
                    {
                        return 365;
                    }
                    if (LANGUAGE == Language.LANG_IT)
                    {
                        return 366;
                    }
                    if (LANGUAGE == Language.LANG_NL)
                    {
                        return 367;
                    }
                    if (LANGUAGE == Language.LANG_BR)
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
                        if (LANGUAGE == Language.LANG_RU)
                        {
                            return 277;
                        }
                        if (LANGUAGE == Language.LANG_DE)
                        {
                            return 278;
                        }
                        if (LANGUAGE == Language.LANG_FR)
                        {
                            return 279;
                        }
                        if (LANGUAGE == Language.LANG_ZH)
                        {
                            return 280;
                        }
                        if (LANGUAGE == Language.LANG_JA)
                        {
                            return 281;
                        }
                        if (LANGUAGE == Language.LANG_KO)
                        {
                            return 371;
                        }
                        if (LANGUAGE == Language.LANG_ES)
                        {
                            return 372;
                        }
                        if (LANGUAGE == Language.LANG_IT)
                        {
                            return 373;
                        }
                        if (LANGUAGE == Language.LANG_NL)
                        {
                            return 374;
                        }
                        if (LANGUAGE == Language.LANG_BR)
                        {
                            return 375;
                        }
                        break;
                    case 100:
                        if (LANGUAGE == Language.LANG_RU)
                        {
                            return 282;
                        }
                        if (LANGUAGE == Language.LANG_DE)
                        {
                            return 283;
                        }
                        if (LANGUAGE == Language.LANG_FR)
                        {
                            return 100;
                        }
                        if (LANGUAGE == Language.LANG_ZH)
                        {
                            return 284;
                        }
                        if (LANGUAGE == Language.LANG_JA)
                        {
                            return 285;
                        }
                        if (LANGUAGE == Language.LANG_KO)
                        {
                            return 369;
                        }
                        if (LANGUAGE == Language.LANG_ES)
                        {
                            return 370;
                        }
                        if (LANGUAGE == Language.LANG_IT)
                        {
                            return 100;
                        }
                        if (LANGUAGE == Language.LANG_NL)
                        {
                            return 100;
                        }
                        if (LANGUAGE == Language.LANG_BR)
                        {
                            return 100;
                        }
                        break;
                    default:
                        switch (r)
                        {
                            case 113:
                                if (LANGUAGE == Language.LANG_RU)
                                {
                                    return 292;
                                }
                                if (LANGUAGE == Language.LANG_DE)
                                {
                                    return 291;
                                }
                                if (LANGUAGE == Language.LANG_FR)
                                {
                                    return 113;
                                }
                                if (LANGUAGE == Language.LANG_ZH)
                                {
                                    return 293;
                                }
                                if (LANGUAGE == Language.LANG_JA)
                                {
                                    return 294;
                                }
                                if (LANGUAGE == Language.LANG_KO)
                                {
                                    return 381;
                                }
                                if (LANGUAGE == Language.LANG_ES)
                                {
                                    return 382;
                                }
                                if (LANGUAGE == Language.LANG_IT)
                                {
                                    return 113;
                                }
                                if (LANGUAGE == Language.LANG_NL)
                                {
                                    return 113;
                                }
                                if (LANGUAGE == Language.LANG_BR)
                                {
                                    return 113;
                                }
                                break;
                            case 114:
                                if (LANGUAGE == Language.LANG_RU)
                                {
                                    return 297;
                                }
                                if (LANGUAGE == Language.LANG_DE)
                                {
                                    return 296;
                                }
                                if (LANGUAGE == Language.LANG_FR)
                                {
                                    return 295;
                                }
                                if (LANGUAGE == Language.LANG_ZH)
                                {
                                    return 298;
                                }
                                if (LANGUAGE == Language.LANG_JA)
                                {
                                    return 299;
                                }
                                if (LANGUAGE == Language.LANG_KO)
                                {
                                    return 383;
                                }
                                if (LANGUAGE == Language.LANG_ES)
                                {
                                    return 384;
                                }
                                if (LANGUAGE == Language.LANG_IT)
                                {
                                    return 385;
                                }
                                if (LANGUAGE == Language.LANG_NL)
                                {
                                    return 386;
                                }
                                if (LANGUAGE == Language.LANG_BR)
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

        public static string XNA_ResName(int resId)
        {
            if (resNames_ == null)
            {
                Dictionary<int, string> dictionary = new()
                {
                    { 0, "zeptolab" },
                    { 1, "loaderbar_full" },
                    { 2, "zeptolab_hd" },
                    { 3, "loaderbar_full_hd" },
                    { 4, "menu_button_default" },
                    { 5, "big_font" },
                    { 6, "small_font" },
                    { 7, "menu_loading" },
                    { 8, "drawing_canvas" },
                    { 9, "menu_drawings_bigpage_markers" },
                    { 10, "menu_button_short" },
                    { 11, "drawings_particles" },
                    { 12, "menu_button_default_hd" },
                    { 13, "big_font_hd" },
                    { 14, "small_font_hd" },
                    { 15, "menu_loading_hd" },
                    { 16, "drawing_canvas_hd" },
                    { 17, "menu_drawings_bigpage_markers_hd" },
                    { 18, "menu_button_short_hd" },
                    { 19, "drawings_particles_hd" },
                    { 20, "menu_strings" },
                    { 21, "tap" },
                    { 22, "bubble_break" },
                    { 23, "bubble" },
                    { 24, "candy_break" },
                    { 25, "monster_chewing" },
                    { 26, "monster_close" },
                    { 27, "monster_open" },
                    { 28, "monster_sad" },
                    { 29, "rope_bleak_1" },
                    { 30, "rope_bleak_2" },
                    { 31, "rope_bleak_3" },
                    { 32, "rope_bleak_4" },
                    { 33, "rope_get" },
                    { 34, "scrape" },
                    { 35, "star_1" },
                    { 36, "star_2" },
                    { 37, "star_3" },
                    { 38, "electric" },
                    { 39, "pump_1" },
                    { 40, "pump_2" },
                    { 41, "pump_3" },
                    { 42, "pump_4" },
                    { 60, "ghost_puff" },
                    { 61, "steam_start" },
                    { 62, "steam_start_2" },
                    { 63, "steam_end" },
                    { 43, "spider_activate" },
                    { 44, "spider_fall" },
                    { 45, "spider_win" },
                    { 46, "wheel" },
                    { 47, "win" },
                    { 48, "gravity_off" },
                    { 49, "gravity_on" },
                    { 50, "candy_link" },
                    { 51, "teleport" },
                    { 52, "bouncer" },
                    { 53, "spike_rotate_in" },
                    { 54, "spike_rotate_out" },
                    { 55, "buzz" },
                    { 56, "scratch_in" },
                    { 57, "scratch_out" },
                    { 64, "lantern_teleport_in" },
                    { 65, "lantern_teleport_out" },
                    { 66, "menu_bgr" },
                    { 67, "menu_button_crystal" },
                    { 68, "menu_popup" },
                    { 69, "menu_logo" },
                    { 70, "menu_level_selection" },
                    { 71, "menu_pack_selection" },
                    { 399, "menu_pack_selection_boxes" },
                    { 401, "menu_content_screen" },
                    { 402, "menu_player_afterplay" },
                    { 403, "menu_player" },
                    { 72, "menu_extra_buttons" },
                    { 73, "menu_extra_buttons_en" },
                    { 74, "menu_shadow" },
                    { 75, "candy_select_fx_demo" },
                    { 76, "menu_processing" },
                    { 77, "menu_promo" },
                    { 78, "menu_promo_banner" },
                    { 79, "menu_bgr_hd" },
                    { 80, "menu_button_crystal_hd" },
                    { 81, "menu_popup_hd" },
                    { 82, "menu_logo_hd" },
                    { 83, "menu_level_selection_hd" },
                    { 84, "menu_pack_selection_hd" },
                    { 400, "menu_pack_selection_boxes_hd" },
                    { 404, "menu_content_screen_hd" },
                    { 405, "menu_player_afterplay_hd" },
                    { 406, "menu_player_hd" },
                    { 85, "menu_extra_buttons_hd" },
                    { 86, "menu_extra_buttons_en_hd" },
                    { 87, "menu_shadow_hd" },
                    { 88, "candy_select_fx_demo_hd" },
                    { 89, "menu_processing_hd" },
                    { 90, "menu_promo_hd" },
                    { 91, "menu_promo_banner_hd" },
                    { 92, "hud_buttons" },
                    { 93, "obj_candy_01" },
                    { 94, "obj_spider" },
                    { 95, "confetti_particles" },
                    { 96, "menu_pause" },
                    { 97, "menu_result" },
                    { 98, "font_numbers_big" },
                    { 99, "menu_result_en" },
                    { 100, "hud_buttons_en" },
                    { 101, "obj_candy_02" },
                    { 102, "obj_candy_03" },
                    { 103, "obj_candy_fx" },
                    { 104, "drawing_hidden" },
                    { 105, "hud_buttons_hd" },
                    { 106, "menu_pause_hd" },
                    { 107, "menu_result_hd" },
                    { 108, "obj_candy_01_hd" },
                    { 109, "obj_spider_hd" },
                    { 110, "obj_vinil_hd" },
                    { 111, "confetti_particles_hd" },
                    { 112, "font_numbers_big_hd" },
                    { 113, "hud_buttons_en_hd" },
                    { 114, "menu_result_en_hd" },
                    { 115, "obj_candy_02_hd" },
                    { 116, "obj_candy_03_hd" },
                    { 117, "obj_candy_fx_hd" },
                    { 118, "drawing_hidden_hd" },
                    { 119, "obj_star_disappear" },
                    { 120, "obj_bubble_flight" },
                    { 121, "obj_bubble_pop" },
                    { 122, "obj_hook_auto" },
                    { 123, "obj_spikes_04" },
                    { 124, "obj_bubble_attached" },
                    { 125, "obj_hook_01" },
                    { 126, "obj_hook_02" },
                    { 127, "obj_star_idle" },
                    { 128, "hud_star" },
                    { 129, "obj_spikes_03" },
                    { 130, "obj_spikes_02" },
                    { 131, "obj_spikes_01" },
                    { 132, "char_animations" },
                    { 133, "char_animations_idle" },
                    { 134, "obj_hook_regulated" },
                    { 135, "obj_electrodes" },
                    { 136, "obj_rotatable_spikes_01" },
                    { 137, "obj_rotatable_spikes_02" },
                    { 138, "obj_rotatable_spikes_03" },
                    { 139, "obj_rotatable_spikes_04" },
                    { 140, "obj_rotatable_spikes_button" },
                    { 141, "obj_vinil" },
                    { 142, "obj_hook_movable" },
                    { 143, "obj_pump" },
                    { 144, "tutorial_signs" },
                    { 145, "obj_hat" },
                    { 146, "obj_bouncer_01" },
                    { 147, "obj_bouncer_02" },
                    { 148, "obj_bee" },
                    { 149, "obj_pollen" },
                    { 150, "char_animations_hd" },
                    { 151, "char_animations_idle_hd" },
                    { 152, "hud_star_hd" },
                    { 153, "obj_bubble_attached_hd" },
                    { 154, "obj_bubble_flight_hd" },
                    { 155, "obj_bubble_pop_hd" },
                    { 156, "obj_electrodes_hd" },
                    { 157, "obj_rotatable_spikes_01_hd" },
                    { 158, "obj_rotatable_spikes_02_hd" },
                    { 159, "obj_rotatable_spikes_03_hd" },
                    { 160, "obj_rotatable_spikes_04_hd" },
                    { 161, "obj_rotatable_spikes_button_hd" },
                    { 162, "obj_hook_01_hd" },
                    { 163, "obj_hook_02_hd" },
                    { 164, "obj_hook_auto_hd" },
                    { 165, "obj_hook_movable_hd" },
                    { 166, "obj_hook_regulated_hd" },
                    { 167, "obj_pump_hd" },
                    { 168, "obj_spikes_01_hd" },
                    { 169, "obj_spikes_02_hd" },
                    { 170, "obj_spikes_03_hd" },
                    { 171, "obj_spikes_04_hd" },
                    { 172, "obj_star_disappear_hd" },
                    { 173, "obj_star_idle_hd" },
                    { 174, "tutorial_signs_hd" },
                    { 175, "obj_hat_hd" },
                    { 176, "obj_bouncer_01_hd" },
                    { 177, "obj_bouncer_02_hd" },
                    { 178, "obj_bee_hd" },
                    { 179, "obj_pollen_hd" },
                    { 188, "bgr_01" },
                    { 189, "char_support_01" },
                    { 190, "bgr_02" },
                    { 191, "char_support_02" },
                    { 192, "bgr_03" },
                    { 193, "char_support_03" },
                    { 194, "bgr_04" },
                    { 195, "char_support_04" },
                    { 196, "bgr_05" },
                    { 197, "char_support_05" },
                    { 198, "bgr_06" },
                    { 199, "char_support_06" },
                    { 200, "bgr_07" },
                    { 201, "char_support_07" },
                    { 202, "bgr_08" },
                    { 203, "char_support_08" },
                    { 204, "bgr_09" },
                    { 205, "char_support_09" },
                    { 206, "bgr_10" },
                    { 207, "char_support_10" },
                    { 208, "bgr_11" },
                    { 209, "char_support_11" },
                    { 210, "bgr_12" },
                    { 211, "char_support_12" },
                    { 212, "bgr_13" },
                    { 213, "char_support_13" },
                    { 214, "bgr_14" },
                    { 215, "char_support_14" },
                    { 216, "bgr_01_cover" },
                    { 217, "bgr_02_cover" },
                    { 218, "bgr_03_cover" },
                    { 219, "bgr_04_cover" },
                    { 220, "bgr_05_cover" },
                    { 221, "bgr_06_cover" },
                    { 222, "bgr_07_cover" },
                    { 223, "bgr_08_cover" },
                    { 224, "bgr_09_cover" },
                    { 225, "bgr_10_cover" },
                    { 226, "bgr_11_cover" },
                    { 227, "bgr_12_cover" },
                    { 228, "bgr_13_cover" },
                    { 229, "bgr_14_cover" },
                    { 230, "bgr_01_hd" },
                    { 231, "char_support_01_hd" },
                    { 232, "bgr_02_hd" },
                    { 233, "char_support_02_hd" },
                    { 234, "bgr_03_hd" },
                    { 235, "char_support_03_hd" },
                    { 236, "bgr_04_hd" },
                    { 237, "char_support_04_hd" },
                    { 238, "bgr_05_hd" },
                    { 239, "char_support_05_hd" },
                    { 240, "bgr_06_hd" },
                    { 241, "char_support_06_hd" },
                    { 242, "bgr_07_hd" },
                    { 243, "char_support_07_hd" },
                    { 244, "bgr_08_hd" },
                    { 245, "char_support_08_hd" },
                    { 246, "bgr_09_hd" },
                    { 247, "char_support_09_hd" },
                    { 248, "bgr_10_hd" },
                    { 249, "char_support_10_hd" },
                    { 250, "bgr_11_hd" },
                    { 251, "char_support_11_hd" },
                    { 252, "bgr_12_hd" },
                    { 253, "char_support_12_hd" },
                    { 254, "bgr_13_hd" },
                    { 255, "char_support_13_hd" },
                    { 256, "bgr_14_hd" },
                    { 257, "char_support_14_hd" },
                    { 258, "bgr_01_cover_hd" },
                    { 259, "bgr_02_cover_hd" },
                    { 260, "bgr_03_cover_hd" },
                    { 261, "bgr_04_cover_hd" },
                    { 262, "bgr_05_cover_hd" },
                    { 263, "bgr_06_cover_hd" },
                    { 264, "bgr_07_cover_hd" },
                    { 265, "bgr_08_cover_hd" },
                    { 266, "bgr_09_cover_hd" },
                    { 267, "bgr_10_cover_hd" },
                    { 268, "bgr_11_cover_hd" },
                    { 269, "bgr_12_cover_hd" },
                    { 270, "bgr_13_cover_hd" },
                    { 271, "bgr_14_cover_hd" },
                    { 58, "menu_music" },
                    { 59, "game_music" },
                    { 272, "menu_extra_buttons_ru" },
                    { 273, "menu_extra_buttons_gr" },
                    { 274, "menu_extra_buttons_fr" },
                    { 275, "menu_extra_buttons_zh" },
                    { 276, "menu_extra_buttons_ja" },
                    { 364, "menu_extra_buttons_ko" },
                    { 365, "menu_extra_buttons_es" },
                    { 366, "menu_extra_buttons_it" },
                    { 367, "menu_extra_buttons_nl" },
                    { 368, "menu_extra_buttons_br" },
                    { 277, "menu_result_ru" },
                    { 278, "menu_result_gr" },
                    { 279, "menu_result_fr" },
                    { 280, "menu_result_zh" },
                    { 281, "menu_result_ja" },
                    { 371, "menu_result_ko" },
                    { 372, "menu_result_es" },
                    { 373, "menu_result_it" },
                    { 374, "menu_result_nl" },
                    { 375, "menu_result_br" },
                    { 282, "hud_buttons_ru" },
                    { 283, "hud_buttons_gr" },
                    { 284, "hud_buttons_zh" },
                    { 285, "hud_buttons_ja" },
                    { 369, "hud_buttons_ko" },
                    { 370, "hud_buttons_es" },
                    { 286, "menu_extra_buttons_fr_hd" },
                    { 287, "menu_extra_buttons_gr_hd" },
                    { 288, "menu_extra_buttons_ru_hd" },
                    { 289, "menu_extra_buttons_zh_hd" },
                    { 290, "menu_extra_buttons_ja_hd" },
                    { 376, "menu_extra_buttons_ko_hd" },
                    { 377, "menu_extra_buttons_es_hd" },
                    { 378, "menu_extra_buttons_it_hd" },
                    { 379, "menu_extra_buttons_nl_hd" },
                    { 380, "menu_extra_buttons_br_hd" },
                    { 291, "hud_buttons_gr_hd" },
                    { 292, "hud_buttons_ru_hd" },
                    { 293, "hud_buttons_zh_hd" },
                    { 294, "hud_buttons_ja_hd" },
                    { 381, "hud_buttons_ko_hd" },
                    { 382, "hud_buttons_es_hd" },
                    { 295, "menu_result_fr_hd" },
                    { 296, "menu_result_gr_hd" },
                    { 297, "menu_result_ru_hd" },
                    { 298, "menu_result_zh_hd" },
                    { 299, "menu_result_ja_hd" },
                    { 383, "menu_result_ko_hd" },
                    { 384, "menu_result_es_hd" },
                    { 385, "menu_result_it_hd" },
                    { 386, "menu_result_nl_hd" },
                    { 387, "menu_result_br_hd" },
                    { 300, "drawing_01" },
                    { 301, "drawing_02" },
                    { 302, "drawing_03" },
                    { 303, "drawing_04" },
                    { 304, "drawing_05" },
                    { 305, "drawing_06" },
                    { 306, "drawing_07" },
                    { 307, "drawing_08" },
                    { 308, "drawing_09" },
                    { 309, "drawing_10" },
                    { 310, "drawing_11" },
                    { 311, "drawing_12" },
                    { 312, "drawing_13" },
                    { 313, "drawing_01_hd" },
                    { 314, "drawing_02_hd" },
                    { 315, "drawing_03_hd" },
                    { 316, "drawing_04_hd" },
                    { 317, "drawing_05_hd" },
                    { 318, "drawing_06_hd" },
                    { 319, "drawing_07_hd" },
                    { 320, "drawing_08_hd" },
                    { 321, "drawing_09_hd" },
                    { 322, "drawing_10_hd" },
                    { 323, "drawing_11_hd" },
                    { 324, "drawing_12_hd" },
                    { 325, "drawing_13_hd" },
                    { 326, "menu_drawings_thumb_page" },
                    { 327, "drawing_canvas_locked" },
                    { 328, "drawing_facebook" },
                    { 329, "drawings_menu_markers" },
                    { 330, "drawings_thumb_01" },
                    { 331, "drawings_thumb_02" },
                    { 332, "drawings_thumb_03" },
                    { 333, "drawings_thumb_04" },
                    { 334, "drawings_thumb_05" },
                    { 335, "drawings_thumb_06" },
                    { 336, "drawings_thumb_07" },
                    { 337, "drawings_thumb_08" },
                    { 338, "drawings_thumb_09" },
                    { 339, "drawings_thumb_10" },
                    { 340, "drawings_thumb_11" },
                    { 341, "drawings_thumb_12" },
                    { 342, "drawings_thumb_13" },
                    { 343, "menu_drawings_bgr" },
                    { 344, "omnom_artist" },
                    { 345, "menu_drawings_thumb_page_hd" },
                    { 346, "drawing_canvas_locked_hd" },
                    { 347, "drawing_facebook_hd" },
                    { 348, "drawings_menu_markers_hd" },
                    { 349, "drawings_thumb_01_hd" },
                    { 350, "drawings_thumb_02_hd" },
                    { 351, "drawings_thumb_03_hd" },
                    { 352, "drawings_thumb_04_hd" },
                    { 353, "drawings_thumb_05_hd" },
                    { 354, "drawings_thumb_06_hd" },
                    { 355, "drawings_thumb_07_hd" },
                    { 356, "drawings_thumb_08_hd" },
                    { 357, "drawings_thumb_09_hd" },
                    { 358, "drawings_thumb_10_hd" },
                    { 359, "drawings_thumb_11_hd" },
                    { 360, "drawings_thumb_12_hd" },
                    { 361, "drawings_thumb_13_hd" },
                    { 362, "menu_drawings_bgr_hd" },
                    { 363, "omnom_artist_hd" },
                    { 388, "menu_scrollbar" },
                    { 389, "menu_button_achiv_cup" },
                    { 390, "menu_leaderboard" },
                    { 391, "empty_achievement" },
                    { 392, "arrows" },
                    { 393, "scotch_tape_1" },
                    { 394, "scotch_tape_2" },
                    { 395, "scotch_tape_3" },
                    { 396, "scotch_tape_4" },
                    { 397, "menu_audio_icons" },
                    { 398, "menu_audio_icons_hd" },
                    { 182, "obj_ghost" },
                    { 180, "obj_ghost_bubbles" },
                    { 183, "obj_ghost_hd" },
                    { 181, "obj_ghost_bubbles_hd" },
                    { 184, "obj_pipe" },
                    { 185, "obj_pipe_hd" },
                    { 187, "obj_lantern_hd" },
                    { 407, "menu_agepopup_bgr" },
                    { 408, "menu_agepopup_bgr_hd" },
                    { 409, "menu_agepopup" },
                    { 410, "menu_agepopup_hd" }
                };
                resNames_ = dictionary;
            }
            _ = resNames_.TryGetValue(handleLocalizedResource(handleWvgaResource(resId)), out string text);
            return text;
        }

        public override NSObject loadResource(int resID, ResourceType resType)
        {
            return base.loadResource(handleLocalizedResource(handleWvgaResource(resID)), resType);
        }

        public override void freeResource(int resID)
        {
            base.freeResource(handleLocalizedResource(handleWvgaResource(resID)));
        }

        public override float getScaleX(int r)
        {
            return CHOOSE3(1.0, 1.5, 2.5);
        }

        public override float getScaleY(int r)
        {
            if (r <= 79)
            {
                if (r is not 66 and not 79)
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
            return CHOOSE3(1.0, 1.649999976158142, 2.6500000953674316);
        IL_0173:
            return CHOOSE3(1.0, 1.5, 2.5);
        }

        private static Dictionary<int, string> resNames_;
    }
}
