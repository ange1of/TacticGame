using System;
using System.Collections.Generic;
using System.Linq;
using Priority_Queue;

namespace game {

    class Initiative {
        public Initiative(BattleArmy _firstArmy, BattleArmy _secondArmy) {
            firstArmy = _firstArmy;
            secondArmy = _secondArmy;

            currentMainScale = new SimplePriorityQueue<BattleUnitsStack, double>();
            currentAwaitingScale = new SimplePriorityQueue<BattleUnitsStack, double>();
            nextMainScale = new SimplePriorityQueue<BattleUnitsStack, double>();
            Random rnd = new Random();
            foreach (var stack in firstArmy.unitsStackList.Concat(secondArmy.unitsStackList)) {
                if (stack.unitsCount == 0) {
                    continue;
                }
                currentMainScale.Enqueue(stack, -(stack.metaUnit.initiative + rnd.NextDouble()/100));
                nextMainScale.Enqueue(stack, -(stack.metaUnit.initiative + rnd.NextDouble()/100));
            }
        }

        private void ChangeInitiative(BattleUnitsStack stack, double newValue) {

            if (currentMainScale.Contains(stack)) {
                currentMainScale.UpdatePriority(stack, -newValue + currentMainScale.GetPriority(stack)%1);
            }
            if (currentAwaitingScale.Contains(stack)) {
                currentAwaitingScale.UpdatePriority(stack, newValue + currentAwaitingScale.GetPriority(stack)%1);
            }
            if (nextMainScale.Contains(stack)) {
                nextMainScale.UpdatePriority(stack, -newValue + nextMainScale.GetPriority(stack)%1);
            }
            
            if (!(currentMainScale.Contains(stack) || currentAwaitingScale.Contains(stack) || nextMainScale.Contains(stack))) {
                throw new InitiativeException("Trying to change initiative of stack which is not in scale");
            }
        }
        
        public BattleUnitsStack NextStack() {
            if (currentMainScale.Count != 0) {
                return currentMainScale.First;
            }
            else if (currentAwaitingScale.Count != 0) {
                return currentAwaitingScale.First;
            }
            return null;
        }

        public void NewRound() {
            UpdateScale();
            if (NextStack() != null) {
                throw new InitiativeException("Current round has not ended");
            }
            currentMainScale = nextMainScale;
            nextMainScale = new SimplePriorityQueue<BattleUnitsStack, double>();
            Random rnd = new Random();
            foreach (var stack in firstArmy.unitsStackList.Concat(secondArmy.unitsStackList)) {
                if (stack.unitsCount == 0) {
                    continue;
                }
                nextMainScale.Enqueue(stack, -(stack.metaUnit.initiative + rnd.NextDouble()/100));
            }
        }

        public void UpdateScale() {
            Random rnd = new Random();
            foreach (var stack in firstArmy.unitsStackList.Concat(secondArmy.unitsStackList)) {
                if (stack.unitsCount > 0) { 
                    // Проверяем, не добавились ли новые стеки (и не возродили ли кого-то)
                    if (!(currentMainScale.Contains(stack) || currentAwaitingScale.Contains(stack) || nextMainScale.Contains(stack))) {
                        if (stack.state == State.NotMadeMove) {
                            currentMainScale.Enqueue(stack, -(stack.metaUnit.initiative + rnd.NextDouble()/100));
                        }
                        else if (stack.state == State.Awaiting) {
                            currentAwaitingScale.Enqueue(stack, stack.metaUnit.initiative + rnd.NextDouble()/100);
                        }
                        
                        nextMainScale.Enqueue(stack, -(stack.metaUnit.initiative + rnd.NextDouble()/100));
                    }
                    // Переводим стеки с изменившимся состоянием в другие шкалы
                    else if (currentMainScale.Contains(stack) && stack.state == State.Awaiting) {
                        ToAwaitingScale(stack);
                    }
                    else if (currentMainScale.Contains(stack) && (stack.state == State.MadeMove || stack.state == State.Defending)) {
                        ToNextRound(stack);
                    }
                    else if (currentAwaitingScale.Contains(stack) && (stack.state == State.MadeMove || stack.state == State.Defending)) {
                        ToNextRound(stack);
                    }

                    // Обновляем измененные приоритеты стеков
                    if (currentMainScale.Contains(stack) && stack.metaUnit.initiative != -Math.Round(currentMainScale.GetPriority(stack)) || currentAwaitingScale.Contains(stack) && stack.metaUnit.initiative != Math.Round(currentAwaitingScale.GetPriority(stack)) || nextMainScale.Contains(stack) && stack.metaUnit.initiative != -Math.Round(nextMainScale.GetPriority(stack))) {
                        ChangeInitiative(stack, stack.metaUnit.initiative);
                    }
                }
                else {
                    // Убираем убитые стеки из шкал
                    if (currentMainScale.Contains(stack)) {
                        currentMainScale.Remove(stack);
                    }
                    else if (currentAwaitingScale.Contains(stack)) {
                        currentAwaitingScale.Remove(stack);
                    }

                    if (nextMainScale.Contains(stack)) {
                        nextMainScale.Remove(stack);
                    }
                }
            }
        }

        private void ToAwaitingScale(BattleUnitsStack stack) {
            double initiativeRandom = -currentMainScale.GetPriority(stack)%1;
            currentMainScale.Remove(stack);
            currentAwaitingScale.Enqueue(stack, stack.metaUnit.initiative + initiativeRandom);
        }

        private void ToNextRound(BattleUnitsStack stack) {
            Random rnd = new Random();
            double initiativeRandom = 0;
            if (currentMainScale.Contains(stack)) {
                initiativeRandom = currentMainScale.GetPriority(stack)%1;
                currentMainScale.Remove(stack);
            }
            else if (currentAwaitingScale.Contains(stack)) {
                initiativeRandom = -currentAwaitingScale.GetPriority(stack)%1;
                currentAwaitingScale.Remove(stack);
            }

            if (!nextMainScale.Contains(stack)) {
                nextMainScale.Enqueue(stack, -(stack.unitsType.initiative + (initiativeRandom == 0 ? rnd.NextDouble()/100 : initiativeRandom)));
            }
        }

        public List<(BattleUnitsStack, double)> GetMainScale() {
            var result = new List<(BattleUnitsStack, double)>();
            foreach (var stack in currentMainScale) {
                result.Add((new BattleUnitsStack(stack), Math.Round(currentMainScale.GetPriority(stack))));
            }
            return result.OrderBy(x => x.Item2).Select(x => (x.Item1, -x.Item2)).ToList();
        }

        public List<(BattleUnitsStack, double)> GetAwaitingScale() {
            var result = new List<(BattleUnitsStack, double)>();
            foreach (var stack in currentAwaitingScale) {
                result.Add((new BattleUnitsStack(stack), Math.Round(currentAwaitingScale.GetPriority(stack))));
            }
            return result.OrderBy(x => x.Item2).ToList();
        }

        public List<(BattleUnitsStack, double)> GetNextScale() {
            var result = new List<(BattleUnitsStack, double)>();
            foreach (var stack in nextMainScale) {
                result.Add((new BattleUnitsStack(stack), Math.Round(nextMainScale.GetPriority(stack))));
            }
            return result.OrderBy(x => x.Item2).Select(x => (x.Item1, -x.Item2)).ToList();
        }

        public override string ToString() {
            return $"Main scale: {{{string.Join(", ", GetMainScale())}}}\n" + 
            $"Awaiting scale : {{{string.Join(", ", GetAwaitingScale())}}}\n" + 
            $"Next step scale : {{{string.Join(", ", GetNextScale())}}}";
        }

        private SimplePriorityQueue<BattleUnitsStack, double> currentMainScale;
        private SimplePriorityQueue<BattleUnitsStack, double> currentAwaitingScale;
        private SimplePriorityQueue<BattleUnitsStack, double> nextMainScale;

        private BattleArmy firstArmy { get; }
        private BattleArmy secondArmy { get; }
    }

}