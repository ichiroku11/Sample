using System;
using System.Collections.Generic;
using System.Text;

namespace SampleTest {
	// todo: EqualityComparer<T>を継承するべき？
	// IEqualityComparerの実装
	public class EqualityComparer<TElement, TKey> : IEqualityComparer<TElement> {
		private readonly Func<TElement, TKey> _keySelector;

		public EqualityComparer(Func<TElement, TKey> keySelector) {
			_keySelector = keySelector;
		}

		// 指定したオブジェクトが等しいかどうか
		public bool Equals(TElement x, TElement y) {
			// 2つが同じインスタンスなら等しい
			if (ReferenceEquals(x, y)) {
				return true;
			}

			// どちかがnullなら等しくない
			if (x == null || y == null) {
				return false;
			}

			return _keySelector(x).Equals(_keySelector(y));
		}

		// 指定したオブジェクトのハッシュコードを取得
		public int GetHashCode(TElement obj) {
			if (obj == null) {
				return 0;
			}

			return _keySelector(obj).GetHashCode();
		}
	}
}
