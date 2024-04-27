using DocumentFormat.OpenXml.Bibliography;
using LatokenBot.Data;
using LatokenBot.Entities;
using MongoDB.Driver;

namespace LatokenBot.Services.Actions;

internal class DocumentService(MongoDatabase userDatabase)
{
    private readonly MongoDatabase _userDatabase = userDatabase;
    private const string documentNameCollection = "documents";

    public IMongoCollection<DocumentEntity> GetAllDocuments()
    {
        var db = userDatabase.GetDatabase();
        var collection = db.GetCollection<DocumentEntity>(documentNameCollection);

        return collection;
    }

    public async Task SaveDocument(DocumentEntity document)
    {
        var collection = GetAllDocuments();

        var filter = Builders<DocumentEntity>.Filter
            .Eq(d => d.Id, document.Id);

        var update = Builders<DocumentEntity>.Update
            .Set(d => d.Title, document.Title)
            .Set(d => d.Content, document.Content);

        await collection.UpdateOneAsync(filter, update);
    }

    public async Task InitializationAbout()
    {
        var collection = GetAllDocuments();

        IEnumerable<DocumentEntity> data = [
            new()
            {
                Title = "ЛАТОКЕН - ЭТО СУПЕРМАРКЕТ АКТИВОВ НА ТЕХНОЛОГИЯХ WEB3 И AI.",
                Content =
                "* #1 по числу активов для трейдинга 3,000+ (Бинанс 400+)\n" +
                "* Топ 25 Крипто биржа по рейтингам CoinmarketCap and CoinGecko\n" +
                "* 4 миллиона Счетов\n" +
                "* 1 миллион платящих пользователей в 2022\n" +
                "Мы делаем простым для каждого возможность Узнать, Обменять, Заработать и Потратить любой криптоактив.\n" +
                "Помогая людям понимать криптоактивы и прогнозировать их цену, мы превращаем их в участников глобального\n" +
                "интеллекта и помогаем стать стейкхолдерами будущего.",
            },
            new()
            {
                Title = "РАБОТА В ЛАТОКЕН - УНИКАЛЬНАЯ ВОЗМОЖНОСТЬ ДЛЯ `СПОРТСМЕНОВ` ВЛЮБЛЕННЫХ В ТЕХНОЛОГИИ.",
                Content =
                "* Быстрый рост через решение нетривиальных задач\n" +
                "* Передовые технологии AIxWEB3\n" +
                "* Глобальный рынок, клиенты в 200+ странах\n" +
                "* Самая успешная компания из СНГ в WEB3\n" +
                "* Удаленная работа, но без дауншифтинга\n" +
                "* Оплата в твёрдой валюте, без привязки к банкам\n" +
                "* Опционы с `откешиванием` криптолетом",
            },
            new()
            {
                Title = "РАБОТА У НАС - ВОЗМОЖНОСТЬ СДВИНУТЬ ПЕРИМЕТР ТЕХНОЛОГИЙ.",
                Content = "Технологии: Мультиагенты?, Трансформеры, AGI?, zk Rollups, Re-Staking, Распределенный AGI?, Мультимодальность, RAG, Layer 3?, Web 2 ->3?, Квантовый  AGI?, DePin?, RWA?, Synthetics, Q learning?.",
            },
            new()
            {
                Title = "НАУЧИМ СДВИГАТЬ ГРАНИЦЫ ВОЗМОЖНОГО. Абдулманап Нурмагомедов, тренер 18 чемпионов мира.",
                Content =
                "«Друзья моего сына, перестали развиваться, идти к целям... Они не желали брать на себя нагрузки высшей спортивной школы. Поэтому я сказал сыну\n" +
                "„Они тебе не друзья, сын“.\n" +
                "Хабиб пытался парировать, что дружат со школы.\n" +
                "Я в тот момент спросил его:\n" +
                "— Они готовы делать объемы, которые делаешь ты?\n" +
                "— Нет.\n" +
                "— Их родители думают о том же, о чем я думаю?\n" +
                "— Нет.\n" +
                "— Как вам тогда идти по пути вместе? Ведь я с каждым годом буду увеличивать тебе нагрузку. Те, кого ты называешь своими друзьями, — они сами и их семьи готовы к таким нагрузкам?\n" +
                "— Нет».",
            },
            new()
            {
                Title = "СПОРТИВНЫЙ ТРЕНЕР ПОДХОДИТ ТЕБЕ ЕСЛИ ЛЮБИШЬ:",
                Content =
                "* Сверхусилие и ответственность решить проблему, когда большинство сдается\n" +
                "* Прямоту в обсуждении проблем и ошибок, недоработок\n" +
                "* Стресс и давление для  ускорения и  креативности\n" +
                "* Технологии чтобы создать будущее, нежели полировать прошлое",
            },
            new()
            {
                Title = "СПОРТИВНЫЙ ТРЕНЕР НЕ ПОДХОДИТ ТЕБЕ ЕСЛИ:",
                Content =
                "* Хочет `непыльную` работу\n" +
                "* Ищет `вторые работы`\n" +
                "* Меняет работу, когда сталкиваетс с трудностями и ответственностью\n" +
                "* Лелеит обиды, чтобы оправдать отлынивание\n" +
                "* Прикрывает коллег или сплетничает, вместо прямоты",
            },
            new()
            {
                Title = "КАЖДЫЙ ОТВЕЧАЕТ ЗА ПОЛЬЗУ В ПРОДЕ ДЛЯ КЛИЕНТОВ.",
                Content =
                "ПОДХОДИШЬ:\n" +
                "* Разберусь и сделаю, расписав проект, задачи, блокеры и репорты.\n" +
                "* Каждую неделю улучшаю продукт или процесс, тестирую.\n" +
                "* Измеряю какую пользу  приносит моя работа.\n" +
                "* Дожму задачу сегодня, чтобы не замусоривать недоделом завтра.\n" +
                "* Нахожу 10х улучшения на хакатонах по суботам.\n " +
                "НЕ ПОДХОДИШЬ:\n" +
                "* Это от меня не зависит\n" +
                "* Я не QA и не должен это тестировать\n" +
                "* Я не аналитик и не должен смотреть как релиз повлиял на конверсию\n" +
                "* Я не менеджер, пусть кто то распишет мне задачи\n" +
                "* Мой рабочий день закончился\n" +
                "* Занимаюсь ритуалами обывателей вместо хакатонов по выходным",
            },
            new()
            {
                Title = "ТЕБЯ ОКРУЖАЮТ ТЕ, КТО ЛЮБЯТ СВЕРХ УСИЛИЯ И РОСТ?",
                Content = "ЭТО СТАНЕТ ПОНЯТНО В ПЕРВУЮ ЖЕ НЕДЕЛЮ ПО ТВОЕМУ УЧАСТИЮ В ХАКАТОНАХ ПО ВЫХОДНЫМ.",
            },
            new()
            {
                Title = "НОВЫЕ КОМПАНИИ НЕ НУЖНЫ, ЕСЛИ ОНИ НЕ СДЕЛАЛИ РЕВОЛЮЦИЮ В КУЛЬТУРЕ.",
                Content =
                "Новым компаниям проще сделать культуру, где команда еще прозрачнее и ответственнее. Благодаря меньшему размеру, опционам и поддерживающим процессам. Это привлекает тех, кто растет чтобы изменить мир и отпугивает безбилетников.\n" +
                "Mark Andreessen, Партнер одного из крупнейших VC фондов.",
            },
            new()
            {
                Title = "КУЛЬТУРА СТАРТАПОВ ОПРЕДЕЛЯЕТСЯ УГРОЗОЙ ГИБЕЛИ В ПОПЫТКЕ СОЗДАТЬ БУДУЩЕЕ.",
                Content = "Ben Horowitz, Партнер одного из крупнейших VC фондов.",
            },
            new()
            {
                Title = "Wartime CEO:",
                Content =
                "* создает и захватывает рынок при постоянной угрозе гибели\n" +
                "* ругается и кричит\n" +
                "* нетерпим к дефокусу\n" +
                "* тренирует команду так, чтобы ей не прострелили зад",
            },
            new()
            {
                Title = "КУЛЬТУРА",
                Content =
                "ПОЛНЫЙ КОММИТМЕНТ РАСТИ, ЧТОБЫ БЫТЬ ПОЛЕЗНЕЕ, `БАЛАНС` И `СЧАСТЬЕ` НЕ ДЛЯ ЧЕМПИОНОВ.\n" +
                "Mark Andreessen, Партнер одного из крупнейших VC фондов a16z ",
            },
            new()
            {
                Title = "Счастья не надо, хотим удовлетворенность, что полезны миру.",
                Content =
                "Баланса нет, есть жизнь где ты учишься быть полезнее. Выбор Свидание или Хакатон в сб определяется тем, где ты больше разовьешься.\n" +
                "Стресс это друг помочь преодолеть препятствие.\n" +
                "Никаких сайд, пет, пед проектов. Полный коммитмент.",
            },
            new()
            {
                Title = "OUR PROCESS. OMMITMENT & ACCOUNTABILITY TO REINFORCE THE SPORTS CULTURE",
                Content =
                "PROJECTS BACKLOG (FATHER, 3-9 MONTH)\n" +
                "MONTHLY COMMITMENT/DEMO\n" +
                "WEEKLY COMMITMENT/DEMO\n" +
                "STANDUPS\n" +
                "MISSION OKRS\n" +
                "Teammates see each day how the tasks of their colleagues deliver weekly and monthly commitments toward mission.\n" +
                "Standups are assaults on impediments to deliver Demo or die.\n" +
                "SPORTS CULTURE ELIMINATES FAKE WORK",
            },
            new()
            {
                Title = "АКТУАЛЬНЫЕ ВАКАНСИИ:",
                Content =
                "* Продакт Разработчик (2 из 4 от 1 года: Python,  React/Node, Python, Java, C#, Kotlin, IOS) -> Супермаркет активов (Python, Java, Kotlin, IOS) / Growth платформа (React/Node.js) / Трейдинг деск (C#)\n" +
                "* Data Engineer (SQL 2+ года, DWH, PBI, Python) -> Data (SQL, DWH, PBI, P﻿ython)\n" +
                "* Операционный Менеджер-Разработчик (Меняющим профессию на разработчика или фанаты автоматизации) -> HR, Sales CRM",
            },
            new()
            {
                Title = "ВОЗМОЖНО ЛАТОКЕН ЛУЧШАЯ В МИРЕ КОМПАНИЯ ДЛЯ ТВОЕГО РОСТА СДВИНУТЬ ПЕРИМЕТР ВОЗМОЖНОГО?",
                Content =
                "ИНТЕРВЬЮ ЧЕРЕЗ ХАКАТОН - `https://deliver.latoken.com/hackathon`\n" +
                "Пройди Test, чтобы подготовиться к интервью - `https://docs.google.com/forms/d/e/1FAIpQLSdlj5aA3fCgGri9GeFC4csj-ZiNKnmorRTHNGeiIJRIbKyUZw/viewform`",
            },
        ];

        await collection.InsertManyAsync(data);
    }

    public async Task InitializationHackaton()
    {
        var collection = GetAllDocuments();

        IEnumerable<DocumentEntity> data = [
            new()
            {
                Title = "ХАКАТОН AIxWEB3 в пятницу 18:00\r\n",
                Content = "Получи опыт внедрения AI и приглашение работать в Латокен на `периметре технологий`",
            },
            new()
            {
                Title = "СДВИНЬ ПЕРИМЕТР ТЕХНОЛОГИЙ: ",
                Content = "Технологии: Мультиагенты?, Трансформеры, AGI?, zk Rollups, Re-Staking, Распределенный AGI?, Мультимодальность, RAG, Layer 3?, Web 2 ->3?, Квантовый AGI?, DePin?, RWA?, Synthetics, Q learning?.\r\n",
            },
            new()
            {
                Title = "РАСПИСАНИЕ",
                Content =
                "Пятница:\n" +
                "18:00 Презентация компании и обсуждение задачи.\n" +
                "Суббота:\n" +
                "17:00 Чекпоинт.\n" +
                "18:00 Демо результатов,\n" +
                "19:00 Объявление победителей, интервью и офферы.",
            },
            new()
            {
                Title = "ПОЛЕЗНЫЕ ССЫЛКИ",
                Content =
                "Регистрация на хакатон - `https://docs.google.com/forms/d/e/1FAIpQLSdlj5aA3fCgGri9GeFC4csj-ZiNKnmorRTHNGeiIJRIbKyUZw/viewform`\n" +
                "Пройди тест - `https://t.me/gpt_web3_hackathon/5280`\n" +
                "О Латокен - `https://deliver.latoken.com/about`",
            },
        ];

        await collection.InsertManyAsync(data);
    }

    public async Task InitializationTest()
    {
        var collection = GetAllDocuments();

        IEnumerable<DocumentEntity> data = [
            new()
            {
                Title = "ТЕСТ AIxWeb3 Хакатон Латокен",
                Content =
                "Проверьте, готовы ли вы к результативному хакатону и интервью с помощью этого короткого теста.\n" +
                "Перед прохождением изучите материалы О Латокен и правила Хакатона (мы используем canva, недоступную в РФ - используйте VPN)",
            },
            new()
            {
                Title = "Какие из этих материалов вы прочитали? (несколько вариантов ответа)",
                Content =
                "1) О Хакатоне deliver.latoken.com/hackathon\n" +
                "2) О Латокен deliver.latoken.com/about\n" +
                "3) Большая часть из #nackedmanagement coda.io/@latoken/latoken-talent/nakedmanagement-88",
            },
            new()
            {
                Title = "Какой призовой фонд на Хакатоне? (несколько вариантов ответа)",
                Content =
                "1) 25,000 Опционов\n" +
                "2) 100,000 Опционов или 10,000 LA\n" +
                "3) Только бесценный опыт",
            },
            new()
            {
                Title = "Что от вас ожидают на хакатоне в первую очередь?",
                Content =
                "1) Показать мои способности узнавать новые технологии\n" +
                "2) Показать работающий сервис\n" +
                "3) Продемонстрировать навыки коммуникации и командной работы",
            },
            new()
            {
                Title = "Что из этого является преимуществом работы в Латокен? (несколько вариантов ответа)",
                Content =
                "1) Быстрый рост через решение нетривиальных задач\n" +
                "2) Передовые технологии AIxWEB3\n" +
                "3) Глобальный рынок, клиенты в 200+ странах\n" +
                "4) Возможность совмещать с другой работой и хобби\n" +
                "5) Самая успешная компания из СНГ в WEB3\n" +
                "6) Удаленная работа, но без давншифтинга\n" +
                "7) Оплата в твердой валюте, без привязки к банкам\n" +
                "8) Опционы с `откешиванием` криптолетом\n" +
                "9) Комфортная среда для свободы творчества\n" +
                "10) Каковы Ваши зарплатные ожидания в USD?\n" +
                "11) Какое расписание Хакатона корректнее?",
            },
            new()
            {
                Title = "Каковы Ваши зарплатные ожидания в USD?",
                Content = "Ответ пользователя",
            },
            new()
            {
                Title = "Какое расписание Хакатона корректнее?",
                Content =
                "1) Пятница: 18:00 Разбор задач. Суббота: 18:00 Демо результатов, 19-00 объявление победителей, интервью и офферы\n" +
                "2) Суббота: 12:00 Презентация компании, 18:00 Презентации результатов проектов",
            },
            new()
            {
                Title = "Каковы признаки `Wartime CEO` согласно крупнейшему венчурному фонду a16z? (несколько вариантов ответа)",
                Content =
                "1) Сосредотачивается на общей картине и дает сотрудникам принимать детальные решения на общей картине и дает команде возможность принимать детальные решения\n" +
                "2) Употребляет ненормативную лексику, кричит, редко говорит спокойным тоном\n" +
                "3) Терпит отклонения от плана, если они связаны с усилиями и творчеством\n" +
                "4) Не терпит отклонений от плана\n" +
                "5) Обучает своих сотрудников для обеспечения их удовлетворенности и карьерного развития\n" +
                "6) Тренерует сотрудников, так чтобы им не прострелили зад на поле боя",
            },
            new()
            {
                Title = "Что Латокен ждет от каждого члена команды? (несколько вариантов ответа)",
                Content =
                "1) Спокойной работы без излишнего стресса\n" +
                "2) Вникания в блокеры вне основного стека, чтобы довести свою задачу до прода\n" +
                "3) Тестирование продукта\n" +
                "4) Субординацию, и не вмешательство чужие дела\n" +
                "5) Вежливость и корректность в коммуникации\n" +
                "6) Измерение результатов\n" +
                "7) Демонстрацию результатов в проде каждую неделю",
            },
            new()
            {
                Title = "Представьте вы на выпускном экзамене. Ваш сосед слева просит вас передать ответы от соседа справа. Вы поможете?",
                Content =
                "1) Да\n" +
                "2) Да, но если преподаватель точно не увидит\n" +
                "3) Да, но только если мне тоже помогут\n" +
                "4) Нет\n" +
                "5) Нет, если мне не дадут посмотреть эти ответы\n" +
                "6) Нет, если это может мне повредить",
            },
            new()
            {
                Title = "Кирпич весит килограмм и еще пол-кирпича. Сколько весит кирпич?",
                Content =
                "1) 1 кг\n" +
                "2) 1.5 кг\n" +
                "3) 2 кг\n" +
                "4) 3 кг",
            },
            new()
            {
                Title = "Напишите ваши `за` и `против` работы в Латокен? Чем подробнее, тем лучше - мы читаем.",
                Content = "Ответ пользователя",
            },
        ];

        await collection.InsertManyAsync(data);
    }

    public async Task InitializationHard()
    {
        var collection = GetAllDocuments();

        IEnumerable<DocumentEntity> data = [
            new()
            {
                Title = "The Principles: Olympics of Freedom and Responsibility to Build the Future",
                Content =
                "Once agreed on the Principles - we agreed on many decisions based on them. Teammates leverage the agreed principles to make their judgements and decisions aligned with the common goal without infinite reconciliations. So they move faster and hit targets.\n" +
                "Valentin Preobrazhenskiy",
            },
            new()
            {
                Title = "Fire good ones for freedom",
                Content =
                "Only candid champions for freedom and responsibility. Fire B or A quit.\n" +
                "Generally, new companies should not exist (`https://www.youtube.com/watch?t=279&v=DAu1Skjj1ZA&feature=youtu.be`). The only reason is their new culture which can solve creativity paradoxes to punch through legacy players.\n" +
                "I commit to:\n" +
                "* Put clients first, ego last - never nurture grievances or selfish interests.\n" +
                "* Demo or Die. Focus to deliver, never seek excuses and remove bad apples doing the opposite.\n" +
                "* Make transparent and accountable work of yourself and teammates to remove freeriders and resolve blockers for sportsman.\n" +
                "* Give candid feedback to level up performance, and eliminate talking behind the back.\n" +
                "* Use any feedback to grow and never give up, never quit.\n" +
                "By doing the above, I will build the culture of Olympic Freedom and Responsibility for mission driven critical hits (`https://coda.io/@latoken/latoken-talent/focus-on-the-critical-hit-89`).​\n" +
                "Apply to join > `https://t.me/latoken_hiring_bot?start=cmVmPVZhbGVudGluX1ByZW9icmF6aGVuc2tpeQ==`",
            },
            new()
            {
                Title = "⁠LATOKEN PRINCIPLES (RULES)",
                Content =
                "Culture is born in the war of economic selection. The war is between the past and the future. Once a greater culture is born, it erases cultures of the past. Past cultures strive to survive and thus the war is imminent and intence as cultu3ricide erases way more information than genocide.\n" +
                "We do not smell this war with rooten flash on the streets.\n" +
                "The war is now on the markets. Cultures compete for clients via products and prices instead of guns. Thus the war is now global and ligtning fast. Evolve culture lightning fast or die. That is evolution.\n" +
                "(To avoid confusion, this text is written in 2020. Update from the CEO “I donate to the war refugees. Any\n" +
                "violation of human rights is a threat to the free markets and humankind. Humans should compete via fair market competition to evolve, otherwise the technology will destroy us.”)\n" +
                "Culture is not something what we have in our hearts. It is not a set of our beliefs. Culture is a set of actions. Who you are is what you do. Culture is how and which decisions are made, especially if no one sees.\n" +
                "We have beliefs and Principles and we will do everything to make them working. We leverage shocking rules to make it clear.\n" +
                "For example, if one is not following the Rule to be the client of the product her is building - her would not be able to build it.\n" +
                "We commit to follow LATOKEN Principles. If we violate any of them we report it, make our best to fix mistake and learn from the failure. We help teammates to follow the principles. If we think one is violating the principles - we will tell about it to her or speak up to her supervisor, and of course would not talk behind a person’s back without her supervisor.",
            },
            new()
            {
                Title = "1. Our Goal of Life is to make the world better with a product",
                Content =
                "Otherwise, what would one do in a startup? Why does startup need tension originated by conflicts of other goals with the innovator’s mission? How would one choose the right priorities? There will be constant tensions with our principles such as Client First, Ego Last or Never waste time or Get out of comfort.\n" +
                "a. Align your daily tasks with the product mission via grandma clear OKRs. If you do not understand the mission - make extra effort to understand. “Company has no vision or mission or culture” is a top excuse of agents to freeride principals.\n" +
                "b. If you need to be inspired - do not do this. Get out.",
            },
            new()
            {
                Title = "2. Clients First, Ego Last",
                Content =
                "a. We are the users. Be the client of the product you are building.Test it, and talk with clients.\n" +
                "b. Align each task and second of your life with the clients success. Architect 200 trln of your synapses and 100x more of synapses around for your life mission lasting beyond your body life. Every evening or weekend you have meaningful time with yourself, teammates, your relatives and friends helpful to achieve your mission. Isn’t it?\n" +
                "c. Feel grievance about clients, not about yourself. Clients are not for serving our egos of being the rulers of the future. Do not defend or excuse our ego.\n" +
                "d. Think big. Count in second- and third-order consequences. Learn from books of top tech founders and investors.\n" +
                "e. Take a reasonable risk. Tolerate mistakes and do not tolerate incompetence. Mistakes are ok, if reported and learned. If you suffer - good, it means you are growing.",
            },
            new()
            {
                Title = "3. Demo or Die. Focus to deliver, never seek excuses and remove bad apples doing the opposite",
                Content =
                "a. Get shit done. Hardwork and achievement is not a pleasant comfort travel, thus most people try to avoid it. Do not allow to sneak out, force to get shit done.\n" +
                "b. Discover the root problem or blocker. Ask “why did it happen 4 times to get in-depth insight into what should be fixed.\n" +
                "c. Crystalise blokers in projects update and solve one by one. If there are no such clarity, it is likely that the doers decided to work to prove that they should not do the task or task is undoable. Do not allow fake work, nor vague defined work.\n" +
                "d. Eliminate low standards or excuse seeking before a Bad apple spoils entire harvest. If others see that avoiding ownership and seeking excuse is a beneficial strategy - high performance strategy is not a Nash stable anymore and we are dead zombies.\n" +
                "e. Have your highest-performance responsibility area, limit jumping around. Know who you are and what your personal mission is.\r\n\t\tf. Double effort when failed. Learning how to get out of shit is the greatest one. Get shit done.",
            },
            new()
            {
                Title = "4. Make transparent and accountable work of yourself and teammates to remove freeriders and resolve blockers for sportsman",
                Content =
                "a. Transparent Work. Projects, Tasks and Work done of each teammate must be clear to you. Each standup you understand who did what yesterday, plan to do today and what are their blockers. Ensure the tasks have real stories Who need What for Why, comments for hours billed are clear and reasonable, ask “stupid questions” if anything is unclear.\n" +
                "b. Make teammates accountable. Grill evasive behaviours to push bad apple and freeriders out while making clear to others to avoid copying the toxic evasive strategy. If weekly Demo is not done - ask why a teammate did not push hard to resolve blockers. Smash attempts to avoid the questions asked or to blur details of the blockers. You critical thinking skills are very important to judge if there are excuses or real independent blockers.\n" +
                "c. Elimintate covering. Agents (bad apples and freeriders) are making friends to cover each other by injectig lies, changing topics when asked unpleasant questions and mutually avoiding writing clear stories, project updates and blockers. Crush them.\n" +
                "d. Report mistakes. If you feel pain due to accountability, this means you are growing. Get our of comfort to grow.",
            },
            new()
            {
                Title = "4. Candid feedback, no politics",
                Content =
                "a. Give open feedback asap. Wrong public feedback is way better than polite tacit disagreement and talking behind our backs - that is prohibited politics. We do not hide our opinions and never talk behind. If we suffer from our openness - this means we are growing. If we see something unethical or if we are unhappy, it is our job to speak up.\n" +
                "b. Eliminate talking behind and gossips. We share with others only what we would say if the other person was in the room. If we discuss a misalignment - it should be heard by the person or her supervisors as we want to correct it. Otherwise it is likely to be collusion against a teammate, and potentially against all teammates and clients. We stop and report talking behind.\n" +
                "c. Prefer productive open conflict to find a great solution over tacit compromise over mediocracy.\r\n\t\td. Get any feedback to grow. It does not matter if feedback is accurate or not. The ultimate feedback - startup and you are dead 10,000 to 1 if you do not do 10x, 10x and 10x better. You are dead means your culture will not be remembered and passed to next generations. Why do you live then?\n" +
                "e. No politics. Negative public feedback should not change you attitude to a person under fire, work with her like with any other teammate.",
            },
            new()
            {
                Title = "5. Navy Seals to Every Role",
                Content =
                "a. Raise the bar with any hire. Newcomers must be better than we are today, so we become greater tomorrow.\n" +
                "b. Hire and raise your successor from day one. That is how you accelerate vesting and value of your stocks.\n" +
                "c. Everyone is responsible for hiring. Managers are promoted based on their ability to attract and develop top talent.\n" +
                "d. Do Sugar Cookie traning. Put a pressure early to learn if the Navy Seal is ready for the real encounter.",
            },
            new()
            {
                Title = "6. Be Ethically Radically Transparent, Yet Keep Our Secrets and Know Hows",
                Content = "a. Grandma should understand your OKRs, tasks, and failures at first glance.\r\n\t\tb. Report mistakes. Hiding mistake espesially with a lie is a crime.\r\n\t\tc. Stories must be “Who needs What for Why”.\r\n\t\td. Give open feedback. We do not hide our opinions and never backtalk. If we suffer from our openness - this means we are growing. If we see something unethical or if we are unhappy, it is our job to speak up.\r\n\t\te. Negative feedback in public meeting is better than no feedback. Especially if one demonstrates negligence to the principles.\r\n\t\tf. We ask for help when getting stuck or feel incompetent.\r\n\t\tg. Keep confidential data under lock such as client personal data, salaries, bonuses, procedures related to security, our conflicts and hurdles.\r\n\t\th. Report to supervisor and CEO if the Principles or Code of Ethics are violated or not working well, report any fake work, any freeriding or cheating.\r\n\t\ti. Speak up to compliance, do not wash our dirty linen in public outside company. Follow NDA contract obligations. Public can not be expert in our internal problems.\r\n",
            },
            new()
            {
                Title = "Почему ты должен присоединиться к Латокену",
                Content =
                "I am joining LATOKEN because I believe in the mission to put money at people’s fingertips to build and co-own the future.\n" +
                "I am joining LATOKEN because I believe in the Principles, will act accordingly to them and will do everything I can to make them work perfectly for the mission.\n" +
                "I will demand from others to perform and use my critical reasoning and high standards if hear I excuses for adequate or lack of performance.",
            },
            new()
            {
                Title = "Principles to benefit clients",
                Content = "Ensure principles are used to build great product for clients. Report if Principles are misused. Slack had “empathy” as a top value, however some employees started to use it against candid feedback... what happened - see in this video. (`https://www.youtube.com/watch?v=YxdZB7tBnlY&t=613s`)⁠",
            },
        ];

        await collection.InsertManyAsync(data);
    }
}